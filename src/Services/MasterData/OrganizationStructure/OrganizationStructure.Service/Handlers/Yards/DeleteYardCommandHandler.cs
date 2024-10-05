using FCC.Core;
using FCC.Core.Constants;
using MediatR;
using Microsoft.Extensions.Logging;
using OrganizationManagementService.Service.Commands.Yard;
using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.Handlers.Yards;
using Softura.EntityFrameworkCore.Abstractions;

namespace OrganizationManagementService.Service.Handlers.Yards
{
    public class DeleteYardCommandHandler : IRequestHandler<DeleteYardCommand, int?>
    {
        private readonly IRepository<Yard> _yardRepository;
        private readonly IRepository<BusinessEntity> _businessEntityRepository;
        private readonly IRepository<BusinessEntityHierarchy> _businessEntityHierarchyRepository;
        private readonly IRepository<BusinessEntityGeneralLedger> _businessEntityGeneralLedgerRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<BusinessEntityAddress> _businessEntityAddressRepository;
        private readonly IRepository<BusinessEntityPhone> _businessEntityPhoneRepository;
        private readonly IRepository<BusinessEntityTerm> _businessEntityTermRepository;
        private readonly IRepository<BusinessEntityCounter> _businessEntityCounterRepository;

        private readonly ILogger<CreateYardCommandHandler> _logger;
        public DeleteYardCommandHandler(IRepository<Yard> yardRepository,
            IRepository<BusinessEntity> businessEntityRepository,
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
            _businessEntityHierarchyRepository = businessEntityHierarchyRepository;
            _businessEntityGeneralLedgerRepository = businessEntityGeneralLedgerRepository;
            _businessEntityAddressRepository = businessEntityAddressRepository;
            _businessEntityPhoneRepository = businessEntityPhoneRepository;
            _addressRepository = addressRepository;
            _businessEntityTermRepository = businessEntityTermRepository;
            _businessEntityCounterRepository = businessEntityCounterRepository;
            _logger = logger;
        }
        public async Task<int?> Handle(DeleteYardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Start DeleteYardCommandHandler, Parameters: BusinessEntityId: {request.UpdateYard.BusinessEntityId}", request.BusinessEntityId);
                if (request.BusinessEntityId != 0)
                {
                    var result = request.BusinessEntityId;

                    var getBusinessEntity = _businessEntityRepository.Get().Where(p => p.BusinessEntityID != request.BusinessEntityId).FirstOrDefault();

                    //update the business entity information
                    if (getBusinessEntity != null)
                    {
                        var businessEntity = new BusinessEntity
                        {
                            BusinessEntityID = request.BusinessEntityId,
                            BusinessEntityTypeId = getBusinessEntity.BusinessEntityTypeId,
                            BusinessEntityName = getBusinessEntity.BusinessEntityName,
                            BusinessEntityCode = getBusinessEntity.BusinessEntityCode,
                            GLDistributionCode = getBusinessEntity.GLDistributionCode,
                            IsActive = false,
                            IsDeleted = true,
                            ModifiedBy = "Administrator",
                            ModifiedDate = DateTime.UtcNow
                        };

                        var updatedBusinessEntity = await _businessEntityRepository.UpdateAsync(businessEntity, cancellationToken);

                        result = updatedBusinessEntity.BusinessEntityID;

                        //update business entity in database
                        if (updatedBusinessEntity.BusinessEntityID == 0)
                            throw new FCCException(ErrorMessage.YARD_UPDATE_ERROR);
                    }
                    //Now update the BusinessEntity Hierarchy
                    var getbusinessEntityHierarchy = _businessEntityHierarchyRepository.Get().Where(p => p.BusinessEntityID == request.BusinessEntityId && !p.IsDeleted).FirstOrDefault();
                    if (getbusinessEntityHierarchy != null)
                    {
                        var businessEntityHierarchy = new BusinessEntityHierarchy
                        {
                            HierarchyId = getbusinessEntityHierarchy.HierarchyId,
                            BusinessEntityID = getbusinessEntityHierarchy.BusinessEntityID,
                            parentBusinessEntityId = getbusinessEntityHierarchy.parentBusinessEntityId,
                            IsActive = false,
                            IsDeleted = true,
                            ModifiedBy = "Administrator",
                            ModifiedDateUTC = DateTime.UtcNow
                        };

                        var updatedBusinessEntityHierarchy = await _businessEntityHierarchyRepository.UpdateAsync(businessEntityHierarchy, cancellationToken);
                        if (updatedBusinessEntityHierarchy.HierarchyId == 0)
                            throw new FCCException(ErrorMessage.BUSINESSENTITYHIERARCHY_UPDATE_ERROR);
                    }

                    //Now update the Yard
                    var getyard = _yardRepository.Get().Where(p => p.BusinessEntityId == request.BusinessEntityId && !p.IsDeleted).FirstOrDefault();
                    if (getyard != null)
                    {
                        var yard = new Yard
                        {
                            BusinessEntityId = getyard.BusinessEntityId,
                            IsActive = false,
                            IsDeleted = true,
                            ModifiedBy = "Administrator",
                            ModifiedDateUTC = DateTime.UtcNow
                        };

                        var updatedYard = await _yardRepository.UpdateAsync(yard, cancellationToken);
                        if (updatedYard.BusinessEntityId == 0)
                            throw new FCCException(ErrorMessage.YARD_UPDATE_ERROR);
                    }

                    //Now update the Business Entity General Ledger
                    var getbusinessEntityGeneralLedger = _businessEntityGeneralLedgerRepository.Get().Where(p => p.BusinessEntityId == request.BusinessEntityId && !p.IsDeleted).FirstOrDefault();
                    if (getbusinessEntityGeneralLedger != null)
                    {
                        var businessEntityGeneralLedger = new BusinessEntityGeneralLedger
                        {
                            BusinessEntityId = getbusinessEntityGeneralLedger.BusinessEntityId,
                            GeneralLedgerId = getbusinessEntityGeneralLedger.GeneralLedgerId,
                            IsActive = false,
                            IsDeleted = true,
                            ModifiedBy = "Administrator",
                            ModifiedDateUTC = DateTime.UtcNow
                        };

                        var updatedledger = await _businessEntityGeneralLedgerRepository.UpdateAsync(businessEntityGeneralLedger, cancellationToken);
                        if (updatedledger.GeneralLedgerId == 0)
                            throw new FCCException(ErrorMessage.BUSINESSENTITYGENERALLEDGER_UPDATE_ERROR);
                    }


                    //Now update the Business Entity Address
                    var getbusinessEntityAddress = _businessEntityAddressRepository.Get().Where(p => p.BusinessEntityId == request.BusinessEntityId && !p.IsDeleted);
                    if (getbusinessEntityAddress != null)
                    {
                        foreach (var item in getbusinessEntityAddress)
                        {
                            var businessEntityAddress = new BusinessEntityAddress
                            {
                                BusinessEntityId = item.BusinessEntityId,
                                AddressTypeId = item.AddressTypeId,
                                AddressId = item.AddressId,
                                IsActive = false,
                                IsDeleted = true,
                                ModifiedBy = "Administrator",
                                ModifiedDateUTC = DateTime.UtcNow
                            };

                            var updatedBusinessEntityAddress = await _businessEntityAddressRepository.UpdateAsync(businessEntityAddress, cancellationToken);
                            if (updatedBusinessEntityAddress.BusinessEntityId == 0)
                                throw new FCCException(ErrorMessage.BUSINESSENTITYADDRESS_UPDATE_ERROR);

                            //Now update the Address
                            var getAddress = _addressRepository.Get().Where(p => p.AddressID == item.AddressId && !p.IsDeleted).FirstOrDefault();
                            if (getAddress != null)
                            {
                                var addresss = new Address
                                {
                                    AddressID = getAddress.AddressID,
                                    AddressLine1 = getAddress.AddressLine1,
                                    AddressLine2 = getAddress.AddressLine2,
                                    City = getAddress.City,
                                    StateProvinceId = getAddress.StateProvinceId,
                                    PostalCode = getAddress.PostalCode,
                                    IsActive = false,
                                    IsDeleted = true,
                                    ModifiedBy = "Administrator",
                                    ModifiedDate = DateTime.UtcNow
                                };
                                var updatedAddress = await _addressRepository.UpdateAsync(addresss, cancellationToken);
                                if (updatedAddress.AddressID == 0)
                                    throw new FCCException(ErrorMessage.ADDRESS_UPDATE_ERROR);
                            }
                        }
                    }

                    //Now update the Business Entity Phone
                    var getbusinessEntityPhone = _businessEntityPhoneRepository.Get().Where(p => p.BusinessEntityId == request.BusinessEntityId && !p.IsDeleted);
                    if (getbusinessEntityPhone != null)
                    {
                        foreach (var item in getbusinessEntityPhone)
                        {
                            var businessEntityPhone = new BusinessEntityPhone
                            {
                                BusinessEntityId = item.BusinessEntityId,
                                PhoneNumberTypeId = item.PhoneNumberTypeId,
                                CountryCode = item.CountryCode,
                                PhoneNumber = item.PhoneNumber,
                                IsActive = false,
                                IsDeleted = true,
                                ModifiedBy = "Administrator",
                                ModifiedDateUTC = DateTime.UtcNow
                            };
                            var createdBusinessEntityPhone = await _businessEntityPhoneRepository.UpdateAsync(businessEntityPhone, cancellationToken);
                            if (createdBusinessEntityPhone.PhoneNumberTypeId == 0)
                                throw new FCCException(ErrorMessage.BUSINESSENTITYPHONE_UPDATE_ERROR);
                        }
                    }

                    //Now update the Terms
                    //Now update the Business Entity Term
                    var getbusinessEntityTerm = _businessEntityTermRepository.Get().Where(p => p.BusinessEntityID == request.BusinessEntityId && !p.IsDeleted);
                    if (getbusinessEntityTerm != null)
                    {
                        foreach (var item in getbusinessEntityTerm)
                        {
                            var businessEntityTerm = new BusinessEntityTerm
                            {
                                BusinessEntityTermID = item.BusinessEntityTermID,
                                TermID = item.TermID,
                                BusinessEntityID = item.BusinessEntityID,
                                IsActive = false,
                                IsDeleted = true,
                                ModifiedBy = "Administrator",
                                ModifiedDateUTC = DateTime.UtcNow
                            };

                            var updatedBusinessEntityTerm = await _businessEntityTermRepository.UpdateAsync(businessEntityTerm, cancellationToken);
                            if (updatedBusinessEntityTerm.TermID == 0)
                                throw new FCCException(ErrorMessage.BUSINESSENTITYTERM_UPDATE_ERROR);
                        }
                    }

                    //Now update the Business Entity Counter
                    var getbusinessEntityCounter = _businessEntityCounterRepository.Get().Where(p => p.BusinessEntityId == request.BusinessEntityId && !p.IsDeleted);
                    if (getbusinessEntityCounter != null)
                    {
                        foreach (var item in getbusinessEntityCounter)
                        {
                            var businessEntityCounter = new BusinessEntityCounter
                            {
                                BusinessEntityId = item.BusinessEntityId,
                                CounterCategoryId = item.CounterCategoryId,
                                CounterValue = item.CounterValue,
                                IsActive = false,
                                IsDeleted = true,
                                ModifiedBy = "Administrator",
                                ModifiedDateUTC = DateTime.UtcNow
                            };
                            var updatedbusinessEntityCounter = await _businessEntityCounterRepository.UpdateAsync(businessEntityCounter, cancellationToken);
                            if (updatedbusinessEntityCounter.CounterCategoryId == 0)
                                throw new FCCException(ErrorMessage.BUSINESSENTITYCOUNTER_UPDATE_ERROR);
                        }
                    }

                    _logger.LogInformation($"DeleteYardCommandHandler End");
                    return result;
                }
                return Convert.ToInt32(ResponseEnum.NotExists);
            }
            catch (Exception)
            {
                _logger.LogError($"Error In YardController.DeleteYardCommandHandler");
                throw;
            }
        }

    }
}
