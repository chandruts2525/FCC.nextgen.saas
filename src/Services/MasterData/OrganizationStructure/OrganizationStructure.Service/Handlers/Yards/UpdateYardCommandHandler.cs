using FCC.Core.Constants;
using FCC.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data;
using Softura.EntityFrameworkCore.Abstractions;
using OrganizationStructure.Service.ViewModel.Yard;
using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.Commands.Yard;
using Microsoft.EntityFrameworkCore;

namespace OrganizationStructure.Service.Handlers.Yards
{
    public class UpdateYardCommandHandler : IRequestHandler<UpdateYardCommand, UpdateYardVM>
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

        private readonly ILogger<UpdateYardCommandHandler> _logger;
        public UpdateYardCommandHandler(IRepository<Yard> yardRepository,
            IRepository<BusinessEntity> businessEntityRepository,
            IRepository<BusinessEntityType> businessEntityTypeRepository,
            IRepository<BusinessEntityHierarchy> businessEntityHierarchyRepository,
            IRepository<BusinessEntityGeneralLedger> businessEntityGeneralLedgerRepository,
            IRepository<Address> addressRepository,
            IRepository<BusinessEntityAddress> businessEntityAddressRepository,
            IRepository<BusinessEntityPhone> businessEntityPhoneRepository,
            IRepository<BusinessEntityTerm> businessEntityTermRepository,
            IRepository<BusinessEntityCounter> businessEntityCounterRepository,
            ILogger<UpdateYardCommandHandler> logger)
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

        public async Task<UpdateYardVM> Handle(UpdateYardCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateYardVM();

            var checkBusinessEntityType = await _businessEntityTypeRepository.Get().Where(x => x.BusinessEntityTypeName!.ToLower() == BuisnessEntityName.Yard.ToString()).FirstOrDefaultAsync(cancellationToken) ??
                throw new FCCException(ErrorMessage.NOT_EXISTS);

            if (!string.IsNullOrEmpty(request.UpdateYard.BusinessEntityName) && request.UpdateYard.BusinessEntityId != 0)
            {
                //update the business entity information
                var businessEntity = new BusinessEntity
                {
                    BusinessEntityID = request.UpdateYard.BusinessEntityId,
                    BusinessEntityTypeId = checkBusinessEntityType.BusinessEntityTypeId,
                    BusinessEntityName = request.UpdateYard.BusinessEntityName,
                    BusinessEntityCode = request.UpdateYard.BusinessEntityCode,
                    GLDistributionCode = request.UpdateYard.GLDistributionCode,
                    IsActive = request.UpdateYard.Status,
                    ModifiedBy = "Administrator",
                    ModifiedDate = DateTime.UtcNow
                };

                _logger.LogInformation("Start UpdateYardCommandHandler, Parameters: BusinessEntityId: {request.UpdateYard.BusinessEntityId}", request.UpdateYard.BusinessEntityId);


                var getbusinessEntity = await _businessEntityRepository.Get(a => a.BusinessEntityID != request.UpdateYard.BusinessEntityId && a.BusinessEntityTypeId == checkBusinessEntityType.BusinessEntityTypeId && !a.IsDeleted).ToListAsync(cancellationToken);
                if (getbusinessEntity?.Count > 0 && getbusinessEntity.Exists(p => p.BusinessEntityName == request.UpdateYard.BusinessEntityName))
                {
                    response.BusinessEntityId = Convert.ToInt32(ResponseEnum.NotExists);
                    response.BusinessEntityName = ErrorMessage.NAME_ALREADY_EXIST_ERROR;
                }
                if (getbusinessEntity?.Count > 0 && getbusinessEntity.Exists(p => p.BusinessEntityCode == request.UpdateYard.BusinessEntityCode))
                {
                    response.BusinessEntityId = Convert.ToInt32(ResponseEnum.NotExists);
                    response.BusinessEntityCode = ErrorMessage.CODE_ALREADY_EXIST_ERROR;
                }

                if (response.BusinessEntityId == 0)
                {
                    var updatedBusinessEntity = await _businessEntityRepository.UpdateAsync(businessEntity, cancellationToken);

                    //update business entity in database

                    if (updatedBusinessEntity.BusinessEntityID == 0)
                        throw new FCCException(ErrorMessage.YARD_UPDATE_ERROR);

                    response = new UpdateYardVM
                    {
                        BusinessEntityId = updatedBusinessEntity.BusinessEntityID,
                        BusinessEntityName = updatedBusinessEntity.BusinessEntityName
                    };

                    //Now update the BusinessEntity Hierarchy
                    var getbusinessEntityHierarchy = _businessEntityHierarchyRepository.Get().Where(p => p.BusinessEntityID == request.UpdateYard.BusinessEntityId && p.parentBusinessEntityId == request.UpdateYard.ParentBusinessEntityId && !p.IsDeleted).FirstOrDefault();
                    if (getbusinessEntityHierarchy != null)
                    {
                        var businessEntityHierarchy = new BusinessEntityHierarchy
                        {
                            HierarchyId = getbusinessEntityHierarchy.HierarchyId,
                            BusinessEntityID = updatedBusinessEntity.BusinessEntityID,
                            parentBusinessEntityId = request.UpdateYard.ParentBusinessEntityId,
                            IsActive = request.UpdateYard.Status,
                            ModifiedBy = "Administrator",
                            ModifiedDateUTC = DateTime.UtcNow
                        };

                        var updatedBusinessEntityHierarchy = await _businessEntityHierarchyRepository.UpdateAsync(businessEntityHierarchy, cancellationToken);
                        if (updatedBusinessEntityHierarchy.HierarchyId == 0)
                            throw new FCCException(ErrorMessage.BUSINESSENTITYHIERARCHY_UPDATE_ERROR);
                    }

                    //Now update the Yard
                    var yard = new Yard
                    {
                        BusinessEntityId = updatedBusinessEntity.BusinessEntityID,
                        IsActive = request.UpdateYard.Status,
                        ModifiedBy = "Administrator",
                        ModifiedDateUTC = DateTime.UtcNow
                    };

                    var updatedYard = await _yardRepository.UpdateAsync(yard, cancellationToken);
                    if (updatedYard.BusinessEntityId == 0)
                        throw new FCCException(ErrorMessage.YARD_UPDATE_ERROR);

                    //Now update the Business Entity General Ledger
                    if (request.UpdateYard.GeneralLedgerId != 0)
                    {
                        var businessEntityGeneralLedger = new BusinessEntityGeneralLedger
                        {
                            BusinessEntityId = updatedBusinessEntity.BusinessEntityID,
                            GeneralLedgerId = request.UpdateYard.GeneralLedgerId,
                            IsActive = request.UpdateYard.Status,
                            ModifiedBy = "Administrator",
                            ModifiedDateUTC = DateTime.UtcNow
                        };

                        var updatedledger = await _businessEntityGeneralLedgerRepository.UpdateAsync(businessEntityGeneralLedger, cancellationToken);
                        if (updatedledger.GeneralLedgerId == 0)
                            throw new FCCException(ErrorMessage.BUSINESSENTITYGENERALLEDGER_UPDATE_ERROR);
                    }

                    //Now update the Address
                    if (request.UpdateYard.Addresses != null && request.UpdateYard.Addresses.Count > 0)
                    {

                        foreach (var item in request.UpdateYard.Addresses)
                        {
                            var getbusinessEntityAddress = _businessEntityAddressRepository.Get().Where(p => p.BusinessEntityId == request.UpdateYard.BusinessEntityId && p.AddressTypeId == item.AddressTypeId && !p.IsDeleted).FirstOrDefault();
                            if (getbusinessEntityAddress != null)
                            {
                                var addresss = new Address
                                {
                                    AddressID = getbusinessEntityAddress.AddressId,
                                    AddressLine1 = item.AddressLine1,
                                    AddressLine2 = item.AddressLine2,
                                    City = item.City,
                                    StateProvinceId = item.StateProvinceId,
                                    PostalCode = item.PostalCode,
                                    IsActive = request.UpdateYard.Status,
                                    ModifiedBy = "Administrator",
                                    ModifiedDate = DateTime.UtcNow
                                };
                                var updatedAddress = await _addressRepository.UpdateAsync(addresss, cancellationToken);
                                if (updatedAddress.AddressID == 0)
                                    throw new FCCException(ErrorMessage.ADDRESS_UPDATE_ERROR);

                                //Now update the Business Entity Address
                                var businessEntityAddress = new BusinessEntityAddress
                                {
                                    BusinessEntityId = updatedBusinessEntity.BusinessEntityID,
                                    AddressTypeId = item.AddressTypeId,
                                    AddressId = updatedAddress.AddressID,
                                    IsActive = request.UpdateYard.Status,
                                    ModifiedBy = "Administrator",
                                    ModifiedDateUTC = DateTime.UtcNow
                                };

                                var updatedBusinessEntityAddress = await _businessEntityAddressRepository.UpdateAsync(businessEntityAddress, cancellationToken);
                                if (updatedBusinessEntityAddress.BusinessEntityId == 0)
                                    throw new FCCException(ErrorMessage.BUSINESSENTITYADDRESS_UPDATE_ERROR);
                            }
                            //Now update the Business Entity Phone
                            var businessEntityPhone = new BusinessEntityPhone
                            {
                                BusinessEntityId = updatedBusinessEntity.BusinessEntityID,
                                PhoneNumberTypeId = item.PhoneNumberTypeId,
                                CountryCode = item.CountryCode,
                                PhoneNumber = item.PhoneNumber,
                                IsActive = request.UpdateYard.Status,
                                ModifiedBy = "Administrator",
                                ModifiedDateUTC = DateTime.UtcNow
                            };
                            var createdBusinessEntityPhone = await _businessEntityPhoneRepository.UpdateAsync(businessEntityPhone, cancellationToken);
                            if (createdBusinessEntityPhone.PhoneNumberTypeId == 0)
                                throw new FCCException(ErrorMessage.BUSINESSENTITYPHONE_UPDATE_ERROR);
                        }
                    }

                    //Now update the Terms
                    if (request.UpdateYard.BusinessEntityTerms != null && request.UpdateYard.BusinessEntityTerms.Count > 0)
                    {
                        foreach (var item in request.UpdateYard.BusinessEntityTerms)
                        {
                            //Now update the Business Entity Term
                            var getbusinessEntityTerm = _businessEntityTermRepository.Get().Where(p => p.BusinessEntityID == request.UpdateYard.BusinessEntityId && p.TermID == item.TermID && !p.IsDeleted).FirstOrDefault();
                            if (getbusinessEntityTerm != null)
                            {
                                var businessEntityTerm = new BusinessEntityTerm
                                {
                                    BusinessEntityTermID = getbusinessEntityTerm.BusinessEntityTermID,
                                    TermID = item.TermID,
                                    BusinessEntityID = updatedBusinessEntity.BusinessEntityID,
                                    IsActive = request.UpdateYard.Status,
                                    ModifiedBy = "Administrator",
                                    ModifiedDateUTC = DateTime.UtcNow
                                };

                                var updatedBusinessEntityTerm = await _businessEntityTermRepository.UpdateAsync(businessEntityTerm, cancellationToken);
                                if (updatedBusinessEntityTerm.TermID == 0)
                                    throw new FCCException(ErrorMessage.BUSINESSENTITYTERM_UPDATE_ERROR);
                            }
                        }
                    }

                    //Now update the Business Entity Counter
                    if (request.UpdateYard.BusinessEntityCounters != null && request.UpdateYard.BusinessEntityCounters.Count > 0)
                    {
                        foreach (var item in request.UpdateYard.BusinessEntityCounters)
                        {
                            var businessEntityCounter = new BusinessEntityCounter
                            {
                                BusinessEntityId = updatedBusinessEntity.BusinessEntityID,
                                CounterCategoryId = item.CounterCategoryId,
                                CounterValue = item.CounterValue,
                                IsActive = request.UpdateYard.Status,
                                ModifiedBy = "Administrator",
                                ModifiedDateUTC = DateTime.UtcNow
                            };
                            var updatedbusinessEntityCounter = await _businessEntityCounterRepository.UpdateAsync(businessEntityCounter, cancellationToken);
                            if (updatedbusinessEntityCounter.CounterCategoryId == 0)
                                throw new FCCException(ErrorMessage.BUSINESSENTITYCOUNTER_UPDATE_ERROR);
                        }
                    }
                }

                _logger.LogInformation($"UpdateYardCommandHandler End");
                return response;
            }
            else
            {
                return response;
            }
        }
    }
}
