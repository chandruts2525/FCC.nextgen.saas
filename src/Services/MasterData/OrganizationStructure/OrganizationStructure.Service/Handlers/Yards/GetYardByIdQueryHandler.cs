using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationManagementService.Service.Queries.Yard;
using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.ViewModel.Yard;
using Softura.EntityFrameworkCore.Abstractions;
using WorkManagement.Domain.Model;

namespace OrganizationStructure.Service.Handlers.Yards
{
    public class GetYardByIdQueryHandler : IRequestHandler<GetYardByIdQuery, GetYardByIdResponseVM>
    {
        private readonly ILogger<GetYardByIdQueryHandler> _logger;
        private readonly IRepository<BusinessEntity> _businessEntityRepository;
        private readonly IRepository<BusinessEntityGeneralLedger> _businessEntityGeneralLedgerRepository;
        private readonly IRepository<BusinessEntityHierarchy> _businessEntityHierarchyRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<StateProvince> _stateProvinceRepository;
        private readonly IRepository<GeneralLedger> _generalLedgerRepository;
        private readonly IRepository<BusinessEntityAddress> _businessEntityAddressRepository;
        private readonly IRepository<BusinessEntityPhone> _businessEntityPhoneRepository;
        private readonly IRepository<Terms> _termRepository;
        private readonly IRepository<BusinessEntityTerm> _businessEntityTermRepository;
        private readonly IRepository<BusinessEntityCounter> _businessEntityCounterRepository;
        public GetYardByIdQueryHandler(IRepository<BusinessEntity> businessEntityRepository,
            IRepository<BusinessEntityHierarchy> businessEntityHierarchyRepository,
            IRepository<BusinessEntityGeneralLedger> businessEntityGeneralLedgerRepository,
            IRepository<Address> addressRepository,
            IRepository<Country> countryRepository,
            IRepository<StateProvince> stateProvinceRepository,
            IRepository<GeneralLedger> generalLedgerRepository,
            IRepository<BusinessEntityAddress> businessEntityAddressRepository,
            IRepository<BusinessEntityPhone> businessEntityPhoneRepository,
            IRepository<Terms> termRepository,
            IRepository<BusinessEntityTerm> businessEntityTermRepository,
            IRepository<BusinessEntityCounter> businessEntityCounterRepository,
            ILogger<GetYardByIdQueryHandler> logger)
        {
            _businessEntityRepository = businessEntityRepository;
            _businessEntityHierarchyRepository = businessEntityHierarchyRepository;
            _businessEntityGeneralLedgerRepository = businessEntityGeneralLedgerRepository; 
            _businessEntityAddressRepository = businessEntityAddressRepository;
            _businessEntityPhoneRepository = businessEntityPhoneRepository;
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
            _stateProvinceRepository = stateProvinceRepository;
            _generalLedgerRepository = generalLedgerRepository;
            _termRepository = termRepository;
            _businessEntityTermRepository = businessEntityTermRepository;
            _businessEntityCounterRepository = businessEntityCounterRepository;
            _logger = logger;
        }

        async Task<GetYardByIdResponseVM> IRequestHandler<GetYardByIdQuery, GetYardByIdResponseVM>.Handle(GetYardByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetYardByIdQueryHandler, Parameters: BusinessEntityId: {request.GetYardById.BusinessEntityId}");

            var result = await (from be in _businessEntityRepository.Get(x => x.BusinessEntityID == request.GetYardById.BusinessEntityId) where !be.IsDeleted && be.IsActive
                                join beh in _businessEntityHierarchyRepository.Get() on be.BusinessEntityID equals beh.BusinessEntityID where !beh.IsDeleted && beh.IsActive
                                join begl in _businessEntityGeneralLedgerRepository.Get() on be.BusinessEntityID equals begl.BusinessEntityId where !begl.IsDeleted && begl.IsActive
                                join gl in _generalLedgerRepository.Get() on begl.GeneralLedgerId equals gl.GeneralLedgerId where !gl.IsDeleted && gl.IsActive
                                select new GetYardByIdResponseVM
                                {
                                    BusinessEntityTypeId = be.BusinessEntityTypeId,
                                    ParentBusinessEntityId = beh.parentBusinessEntityId,
                                    BusinessEntityCode = be.BusinessEntityCode,
                                    BusinessEntityName = be.BusinessEntityName,
                                    GLDistributionCode = be.GLDistributionCode,
                                    GeneralLedgerId = begl.GeneralLedgerId,
                                    GLDescription = gl.Description,
                                    Status = be.IsActive,
                                }).FirstOrDefaultAsync(cancellationToken);


            _logger.LogInformation($"GetYardByIdQueryHandler End");

            if (result != null)
            {
                result.Addresses = await GetAddress(request, cancellationToken);
                result.Term = await GetTerms(request, cancellationToken);
                result.BusinessEntityCounters = await GetBusinessEntityCounters(request, cancellationToken);
            }
            _logger.LogInformation($"GetUserQueryHandler End");

            if (result != null)
                return result;
            else
                return new GetYardByIdResponseVM();
        }

        private async Task<List<AddressVM>> GetAddress(GetYardByIdQuery request, CancellationToken cancellationToken)
        {
            return await (from bea in _businessEntityAddressRepository.Get(x => x.BusinessEntityId == request.GetYardById.BusinessEntityId) where !bea.IsDeleted && bea.IsActive
                          join ad in _addressRepository.Get() on bea.AddressId equals ad.AddressID where !ad.IsDeleted && ad.IsActive
                          join sp in _stateProvinceRepository.Get() on ad.StateProvinceId equals sp.StateProvinceId where !sp.IsDeleted && sp.IsActive
                          join c in _countryRepository.Get() on sp.CountryCode equals c.CountryCode where !c.IsDeleted && c.IsActive
                          join bep in _businessEntityPhoneRepository.Get(x => x.BusinessEntityId == request.GetYardById.BusinessEntityId) on bea.AddressTypeId equals bep.PhoneNumberTypeId
                          where !bep.IsDeleted && bep.IsActive
                          select new AddressVM
                          {
                              AddressId = ad.AddressID,
                              AddressTypeId = bea.AddressTypeId,
                              AddressLine1 = ad.AddressLine1,
                              AddressLine2 = ad.AddressLine2,
                              StateProvinceId = ad.StateProvinceId,
                              StateProvinceName = sp.StateProvinceName,
                              StateProvinceCode =sp.StateProvinceCode,
                              City = ad.City,
                              PostalCode = ad.PostalCode,
                              PhoneNumberTypeId = bep.PhoneNumberTypeId,
                              CountryCode = bep.CountryCode,
                              PhoneNumber = bep.PhoneNumber,
                              PhoneCountryCode = c.CountryCode,
                              CountryName = c.CountryName,
                          }).ToListAsync(cancellationToken);
        }

        private async Task<List<TermsVM>> GetTerms(GetYardByIdQuery request, CancellationToken cancellationToken)
        {
            return await (from bet in _businessEntityTermRepository.Get(x => x.BusinessEntityID == request.GetYardById.BusinessEntityId) where !bet.IsDeleted && bet.IsActive
                          join t in _termRepository.Get() on bet.TermID equals t.TermID
                          where !t.IsDeleted && t.IsActive
                          select new TermsVM
                          {
                              TermID = t.TermID,
                              TermTypeID = t.TermTypeID,
                              TermText = t.TermText,
                              TermName = t.TermName,
                          }).ToListAsync(cancellationToken);
        }

        private async Task<List<BusinessEntityCounterVM>> GetBusinessEntityCounters(GetYardByIdQuery request, CancellationToken cancellationToken)
        {
            return await (from bec in _businessEntityCounterRepository.Get(x => x.BusinessEntityId == request.GetYardById.BusinessEntityId)
                          where !bec.IsDeleted && bec.IsActive
                          select new BusinessEntityCounterVM
                          {
                              CounterCategoryId = bec.CounterCategoryId,
                              CounterValue = bec.CounterValue,
                          }).ToListAsync(cancellationToken);
        }
    }
}
