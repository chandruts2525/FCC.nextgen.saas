using MediatR;
using OrganizationStructure.Service.ViewModel.Yard;
using Softura.EntityFrameworkCore.Abstractions;
using Microsoft.Extensions.Logging;
using FCC.Core.Constants;
using OrganizationStructure.Domain.Model;
using FCC.Core;
using OrganizationManagementService.Service.Commands.Yard;
using Microsoft.EntityFrameworkCore;

namespace OrganizationStructure.Service.Handlers.Yards
{
    public class CreateYardCommandHandler : IRequestHandler<CreateYardCommand, CreateYardVM>
    {
        private readonly IRepository<Yard> _yardRepository;
        private readonly IRepository<BusinessEntity> _businessEntityRepository;
        private readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
        private readonly IRepository<BusinessEntityHierarchy> _businessEntityHierarchyRepository;
        private readonly IRepository<BusinessEntityGeneralLedger> _businessEntityGeneralLedgerRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<BusinessEntityAddress> _businessEntityAddressRepository;
        private readonly IRepository<BusinessEntityPhone> _businessEntityPhoneRepository;
        private readonly IRepository<BusinessEntityTerm> _businessEntityTermRepository;
        private readonly IRepository<BusinessEntityCounter> _businessEntityCounterRepository;

        private readonly ILogger<CreateYardCommandHandler> _logger;
        public CreateYardCommandHandler(IRepository<Yard> yardRepository,
            IRepository<BusinessEntity> businessEntityRepository,
            IRepository<BusinessEntityType> businessEntityTypeRepository,
            IRepository<BusinessEntityHierarchy> businessEntityHierarchyRepository,
            IRepository<BusinessEntityGeneralLedger> businessEntityGeneralLedgerRepository,
            IRepository<Address> addressRepository,
            IRepository<BusinessEntityAddress> businessEntityAddressRepository,
            IRepository<BusinessEntityPhone> businessEntityPhoneRepository,
            IRepository<BusinessEntityTerm> businessEntityTermRepository,
            IRepository<BusinessEntityCounter> businessEntityCounterRepository,
            ILogger<CreateYardCommandHandler> logger)
        {
            _yardRepository = yardRepository;
            _businessEntityRepository = businessEntityRepository;
            _businessEntityTypeRepository = businessEntityTypeRepository;
            _businessEntityHierarchyRepository = businessEntityHierarchyRepository;
            _businessEntityGeneralLedgerRepository = businessEntityGeneralLedgerRepository;
            _businessEntityAddressRepository = businessEntityAddressRepository;
            _businessEntityPhoneRepository = businessEntityPhoneRepository;
            _addressRepository = addressRepository;
            _businessEntityTermRepository = businessEntityTermRepository;
            _businessEntityCounterRepository = businessEntityCounterRepository;
            _logger = logger;
        }

        public async Task<CreateYardVM> Handle(CreateYardCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateYardVM();

            _logger.LogInformation("Start CreateYardCommandHandler, Parameters: BusinessEntityID: {request.CreateYard.BusinessEntityId}", request.CreateYard.BusinessEntityId);

            var checkBusinessEntityType = await _businessEntityTypeRepository.Get().Where(x => x.BusinessEntityTypeName!.ToLower() == BuisnessEntityName.Yard.ToString()).FirstOrDefaultAsync(cancellationToken) ??
                throw new FCCException(ErrorMessage.NOT_EXISTS);


            if (!string.IsNullOrEmpty(request.CreateYard.BusinessEntityName) && !string.IsNullOrEmpty(request.CreateYard.BusinessEntityCode) && checkBusinessEntityType.BusinessEntityTypeId != 0)
            {
                //Save the business entity information
                var businessEntity = new BusinessEntity
                {
                    BusinessEntityTypeId = checkBusinessEntityType.BusinessEntityTypeId,
                    BusinessEntityName = request.CreateYard.BusinessEntityName,
                    BusinessEntityCode = request.CreateYard.BusinessEntityCode,
                    GLDistributionCode = request.CreateYard.GLDistributionCode,
                    IsActive = request.CreateYard.Status,
                };

                //Validate if the name already exists in database

                var getbusinessEntity = await _businessEntityRepository.Get(a => a.BusinessEntityTypeId == checkBusinessEntityType.BusinessEntityTypeId && !a.IsDeleted).ToListAsync(cancellationToken);
                if (getbusinessEntity?.Count > 0 && getbusinessEntity.Exists(p => p.BusinessEntityName == request.CreateYard.BusinessEntityName))
                {
                    response.BusinessEntityId = Convert.ToInt32(ResponseEnum.NotExists);
                    response.BusinessEntityName = ErrorMessage.NAME_ALREADY_EXIST_ERROR;
                }
                if (getbusinessEntity?.Count > 0 && getbusinessEntity.Exists(p => p.BusinessEntityCode == request.CreateYard.BusinessEntityCode))
                {
                    response.BusinessEntityId = Convert.ToInt32(ResponseEnum.NotExists);
                    response.BusinessEntityCode = ErrorMessage.CODE_ALREADY_EXIST_ERROR;
                }

                if (response.BusinessEntityId == 0)
                {
                    var createdBusinessEntity = await _businessEntityRepository.CreateAsync(businessEntity, cancellationToken);

                    //save business entity in database

                    if (createdBusinessEntity.BusinessEntityID == 0)
                        throw new FCCException(ErrorMessage.YARD_SAVE_ERROR);

                    response = new CreateYardVM
                    {
                        BusinessEntityId = createdBusinessEntity.BusinessEntityID,
                        BusinessEntityName = createdBusinessEntity.BusinessEntityName
                    };

                    //Now save the BusinessEntity Hierarchy
                    var businessEntityHierarchy = new BusinessEntityHierarchy
                    {
                        BusinessEntityID = createdBusinessEntity.BusinessEntityID,
                        parentBusinessEntityId = request.CreateYard.ParentBusinessEntityId,
                        IsActive = request.CreateYard.Status
                    };

                    var createdBusinessEntityHierarchy = await _businessEntityHierarchyRepository.CreateAsync(businessEntityHierarchy, cancellationToken);
                    if (createdBusinessEntityHierarchy.HierarchyId == 0)
                        throw new FCCException(ErrorMessage.BUSINESSENTITYHIERARCHY_SAVE_ERROR);

                    //Now save the Yard
                    var yard = new Yard
                    {
                        BusinessEntityId = createdBusinessEntity.BusinessEntityID,
                        IsActive = request.CreateYard.Status
                    };

                    var createdYard = await _yardRepository.CreateAsync(yard, cancellationToken);
                    if (createdYard.BusinessEntityId == 0)
                        throw new FCCException(ErrorMessage.YARD_SAVE_ERROR);

                    //Now save the Business Entity General Ledger
                    if (request.CreateYard.GeneralLedgerId != 0)
                    {
                        var businessEntityGeneralLedger = new BusinessEntityGeneralLedger
                        {
                            BusinessEntityId = createdBusinessEntity.BusinessEntityID,
                            GeneralLedgerId = request.CreateYard.GeneralLedgerId,
                            IsActive = request.CreateYard.Status
                        };

                        var createdledger = await _businessEntityGeneralLedgerRepository.CreateAsync(businessEntityGeneralLedger, cancellationToken);
                        if (createdledger.GeneralLedgerId == 0)
                            throw new FCCException(ErrorMessage.BUSINESSENTITYGENERALLEDGER_SAVE_ERROR);
                    }
                    //Now save the Address
                    if (request.CreateYard.Addresses != null && request.CreateYard.Addresses.Count > 0)
                    {
                        foreach (var item in request.CreateYard.Addresses)
                        {
                            var addresss = new Address
                            {
                                AddressID = item.AddressId,
                                AddressLine1 = item.AddressLine1,
                                AddressLine2 = item.AddressLine2,
                                City = item.City,
                                StateProvinceId = item.StateProvinceId,
                                PostalCode = item.PostalCode,
                                IsActive = request.CreateYard.Status
                            };

                            var createdAddress = await _addressRepository.CreateAsync(addresss, cancellationToken);
                            if (createdAddress.AddressID == 0)
                                throw new FCCException(ErrorMessage.ADDRESS_SAVE_ERROR);


                            //Now save the Business Entity Address
                            var businessEntityAddress = new BusinessEntityAddress
                            {
                                BusinessEntityId = createdBusinessEntity.BusinessEntityID,
                                AddressTypeId = item.AddressTypeId,
                                AddressId = createdAddress.AddressID,
                                IsActive = request.CreateYard.Status
                            };

                            var createdBusinessEntityAddress = await _businessEntityAddressRepository.CreateAsync(businessEntityAddress, cancellationToken);
                            if (createdBusinessEntityAddress.BusinessEntityId == 0)
                                throw new FCCException(ErrorMessage.BUSINESSENTITYADDRESS_SAVE_ERROR);


                            //Now save the Business Entity Phone
                            if (!string.IsNullOrEmpty(item.PhoneNumber))
                            {
                                var businessEntityPhone = new BusinessEntityPhone
                                {
                                    BusinessEntityId = createdBusinessEntity.BusinessEntityID,
                                    PhoneNumberTypeId = item.PhoneNumberTypeId,
                                    CountryCode = item.CountryCode,
                                    PhoneNumber = item.PhoneNumber,
                                    IsActive = request.CreateYard.Status
                                };

                                var createdBusinessEntityPhone = await _businessEntityPhoneRepository.CreateAsync(businessEntityPhone, cancellationToken);
                                if (createdBusinessEntityPhone.PhoneNumberTypeId == 0)
                                    throw new FCCException(ErrorMessage.BUSINESSENTITYPHONE_SAVE_ERROR);
                            }
                        }
                    }

                    //Now save the Terms
                    if (request.CreateYard.BusinessEntityTerms != null && request.CreateYard.BusinessEntityTerms.Count > 0)
                    {
                        foreach (var item in request.CreateYard.BusinessEntityTerms)
                        {
                            //Now save the Business Entity Term
                            var businessEntityTerm = new BusinessEntityTerm
                            {
                                TermID = item.TermID,
                                BusinessEntityID = createdBusinessEntity.BusinessEntityID,
                                IsActive = request.CreateYard.Status
                            };

                            var createdBusinessEntityTerm = await _businessEntityTermRepository.CreateAsync(businessEntityTerm, cancellationToken);
                            if (createdBusinessEntityTerm.TermID == 0)
                                throw new FCCException(ErrorMessage.BUSINESSENTITYTERM_SAVE_ERROR);
                        }
                    }

                    //Now save the Business Entity Counter
                    if (request.CreateYard.BusinessEntityCounters != null && request.CreateYard.BusinessEntityCounters.Count > 0)
                    {
                        foreach (var item in request.CreateYard.BusinessEntityCounters)
                        {
                            var businessEntityCounter = new BusinessEntityCounter
                            {
                                BusinessEntityId = createdBusinessEntity.BusinessEntityID,
                                CounterCategoryId = item.CounterCategoryId,
                                CounterValue = item.CounterValue,
                                IsActive = request.CreateYard.Status
                            };
                            var createdbusinessEntityCounter = await _businessEntityCounterRepository.CreateAsync(businessEntityCounter, cancellationToken);
                            if (createdbusinessEntityCounter.CounterCategoryId == 0)
                                throw new FCCException(ErrorMessage.BUSINESSENTITYCOUNTER_SAVE_ERROR);
                        }
                    }
                }



                _logger.LogInformation($"CreateSecurityYardCommandHandler End");
                return response;
            }
            else
            {
                return response;
            }
        }
    }
}
