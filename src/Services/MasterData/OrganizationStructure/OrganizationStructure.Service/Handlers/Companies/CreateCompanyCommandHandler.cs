using OrganizationStructure.Domain.ViewModel.Company;
using OrganizationStructure.Service.Commands.Company;
using MediatR;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using OrganizationStructure.Domain.Model;
using Microsoft.EntityFrameworkCore;
using FCC.Core.Constants;
using Softura.Core.Extensions;
using FCC.Core;

namespace OrganizationManagementService.Service.Handlers.Companies;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CreateCompanyResponseVM<int>>
{
    private readonly IRepository<Company> _companyRepository;
    private readonly IRepository<BusinessEntity> _businessEntityRepository;
    private readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
    private readonly IRepository<BusinessEntityPhone> _businessEntityPhoneRepository;
    private readonly IRepository<Address> _addressRepository;
    private readonly IRepository<AddressType> _addressTypeRepository;
    private readonly IRepository<ContactType> _contactTypeRepository;
    private readonly IRepository<BusinessEntityCounter> _businessEntityCounterRepository;
    private readonly IRepository<StateProvince> _stateProvinceRepository;
    private readonly IRepository<CompanyDomain> _companyDomainRepository;
    private readonly IRepository<BusinessEntityContact> _businessEntityContactRepository;
    private readonly IRepository<BusinessEntityAddress> _businessEntityAddressRepository;
    private readonly IRepository<BusinessEntityTerm> _businessEntityTermRepository;
    private readonly IRepository<PhoneNumberType> _phoneNumberTypeRepository;
    private readonly IRepository<BusinessEntityHierarchy> _businessEntityHierarchyRepository;

    private readonly ILogger<CreateCompanyCommandHandler> _logger;
    public CreateCompanyCommandHandler(ILogger<CreateCompanyCommandHandler> logger,
        IRepository<BusinessEntityPhone> businessEntityPhoneRepository,
        IRepository<Address> addressRepository,
        IRepository<BusinessEntityAddress> businessEntityAddressRepository,
        IRepository<AddressType> addressTypeRepository,
        IRepository<ContactType> contactTypeRepository,
        IRepository<StateProvince> stateProvinceRepository,
        IRepository<CompanyDomain> companyDomainRepository,
        IRepository<BusinessEntityContact> businessEntityContactRepository,
        IRepository<BusinessEntityCounter> businessEntityCounterRepository,
        IRepository<Company> companyRepository,
        IRepository<BusinessEntity> businessEntityRepository,
        IRepository<BusinessEntityType> businessEntityTypeRepository,
        IRepository<BusinessEntityTerm> businessEntityTermRepository,
        IRepository<BusinessEntityHierarchy> businessEntityHierarchyRepository,
        IRepository<PhoneNumberType> phoneNumberTypeRepository)
    {

        _logger = logger;
        _businessEntityPhoneRepository = businessEntityPhoneRepository;
        _addressRepository = addressRepository;
        _contactTypeRepository = contactTypeRepository;
        _stateProvinceRepository = stateProvinceRepository;
        _companyDomainRepository = companyDomainRepository;
        _businessEntityContactRepository = businessEntityContactRepository;
        _addressTypeRepository = addressTypeRepository;
        _businessEntityAddressRepository = businessEntityAddressRepository;
        _businessEntityCounterRepository = businessEntityCounterRepository;
        _companyRepository = companyRepository;
        _businessEntityRepository = businessEntityRepository;
        _businessEntityTypeRepository = businessEntityTypeRepository;
        _businessEntityTermRepository = businessEntityTermRepository;
        _phoneNumberTypeRepository = phoneNumberTypeRepository;
        _businessEntityHierarchyRepository = businessEntityHierarchyRepository;
    }

    public async Task<CreateCompanyResponseVM<int>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start CreateCompanyCommandHandler, Parameters: ", request.createCompanyVM);
        var companyDetails = request.createCompanyVM.companyDetailsVM;
        var companyLocalization = request.createCompanyVM.companyLocalizationVM;
        var businessEntityType = await _businessEntityTypeRepository.Get(a => a.BusinessEntityTypeName == BuisnessEntityName.Company.ToString()).FirstOrDefaultAsync(cancellationToken);
        var businessEntities = await _businessEntityRepository.Get().Where(a => businessEntityType != null && a.BusinessEntityTypeId == businessEntityType.BusinessEntityTypeId).ToListAsync(cancellationToken);

        var addressType = await _addressTypeRepository.Get().ToListAsync(cancellationToken);
        var stateProvince = await _stateProvinceRepository.Get().ToListAsync(cancellationToken);
        var contactType = await _contactTypeRepository.Get(a => a.ContactTypeName == "CompanyType").FirstOrDefaultAsync(cancellationToken);
        var phoneNumberType = await _phoneNumberTypeRepository.Get().ToListAsync(cancellationToken);

        if (companyDetails?.CompanyName != "")
        {
            #region Company Details

            if (businessEntities.Exists(a => a.BusinessEntityName == companyDetails?.CompanyName && a.IsActive && !a.IsDeleted))
            {
                return new CreateCompanyResponseVM<int>(ErrorMessage: ErrorMessage.COMPANY_ALREADY_EXIST_ERROR);
            }
            BusinessEntity businessEntity = new();
            businessEntity.BusinessEntityName = companyDetails?.CompanyName;
            businessEntity.BusinessEntityCode = companyDetails?.CodeName;
            businessEntity.BusinessEntityTypeId = businessEntityType != null ? businessEntityType.BusinessEntityTypeId : 0;
            businessEntity.IsActive = companyDetails == null || companyDetails.Status;

            businessEntity = await _businessEntityRepository.CreateAsync(businessEntity, cancellationToken);

            //Now save the BusinessEntity Hierarchy
            var businessEntityHierarchy = new BusinessEntityHierarchy
            {
                BusinessEntityID = businessEntity.BusinessEntityID,
                parentBusinessEntityId = request.createCompanyVM.ParentBusinessEntityId,
                IsActive = companyDetails == null || companyDetails.Status
        };

            var createdBusinessEntityHierarchy = await _businessEntityHierarchyRepository.CreateAsync(businessEntityHierarchy, cancellationToken);
            if (createdBusinessEntityHierarchy.HierarchyId == 0)
                throw new FCCException(ErrorMessage.BUSINESSENTITYHIERARCHY_SAVE_ERROR);

            BusinessEntityPhone businessEntityPhone = new()
            {
                BusinessEntityId = businessEntity.BusinessEntityID,
                PhoneNumber = companyDetails?.CompanyPhone,
                CountryCode = companyDetails?.CountryCode,
                IsActive = true,
                PhoneNumberTypeId = phoneNumberType.Find(a => a.PhoneNumberTypeName == PhoneNumberTypes.CompanyPhone.GetDescription()).PhoneNumberTypeId
            };

            await _businessEntityPhoneRepository.CreateAsync(businessEntityPhone, cancellationToken);

            #region Address

            Address businessAddress = new()
            {
                AddressLine1 = companyDetails?.BusinessAddress?.Address1,
                AddressLine2 = companyDetails?.BusinessAddress?.Address2,
                City = companyDetails?.BusinessAddress?.City,
                PostalCode = companyDetails?.BusinessAddress?.Zip,
                IsActive = true,
                StateProvinceId = stateProvince.Find(a => a.CountryCode != null && a.CountryCode.Trim().ToString() == companyDetails.BusinessAddress.Country && a.StateProvinceCode != null && a.StateProvinceCode.Trim().ToString() == companyDetails?.BusinessAddress?.State).StateProvinceId,
            };
            businessAddress = await _addressRepository.CreateAsync(businessAddress, cancellationToken);


            BusinessEntityAddress businessEntity1 = new()
            {
                AddressId = businessAddress.AddressID,
                AddressTypeId = addressType.Find(a => a.AddressTypeName != null && a.AddressTypeName == AddressTypes.BusinessAddress.GetDescription()).AddressTypeId,
                BusinessEntityId = businessEntity.BusinessEntityID,
            };
            await _businessEntityAddressRepository.CreateAsync(businessEntity1, cancellationToken);

            Address MailingAddress = new()
            {
                AddressLine1 = companyDetails?.MailingAddress?.Address1,
                AddressLine2 = companyDetails?.MailingAddress?.Address2,
                City = companyDetails?.MailingAddress?.City,
                PostalCode = companyDetails?.MailingAddress?.Zip,
                IsActive = true,
                StateProvinceId = stateProvince.Find(a => a.CountryCode != null && a.CountryCode.Trim().ToString() == companyDetails?.MailingAddress?.Country && a.StateProvinceCode != null && a.StateProvinceCode.Trim().ToString() == companyDetails?.MailingAddress?.State).StateProvinceId,
            };
            MailingAddress = await _addressRepository.CreateAsync(MailingAddress, cancellationToken);
            BusinessEntityAddress businessEntity2 = new()
            {
                AddressId = MailingAddress.AddressID,
                AddressTypeId = addressType.Find(a => a.AddressTypeName == AddressTypes.MailingAddress.GetDescription()).AddressTypeId,
                BusinessEntityId = businessEntity.BusinessEntityID,
                IsActive = true,
            };

            await _businessEntityAddressRepository.CreateAsync(businessEntity2, cancellationToken);
            #endregion Address

            foreach(var identifier in companyDetails.DomainIdentifier)
            {
                CompanyDomain companyDomain = new()
                {
                    BusinessEntityID = businessEntity.BusinessEntityID,
                    DomainIdentifier = identifier.Identifier,
                    IsActive = true
                };
                await _companyDomainRepository.CreateAsync(companyDomain, cancellationToken);
            }
            

            BusinessEntityContact businessEntityContact = new()
            {
                BusinessEntityID = businessEntity.BusinessEntityID,
                ContactName = companyDetails?.contactInformation?.ContactName,
                CountryCode = companyDetails?.contactInformation?.CountryCode,
                EmailAddress = companyDetails?.contactInformation?.EmailAddress,
                PhoneNumber = companyDetails?.contactInformation?.PhoneNumber,
                ContactTypeID = contactType != null ? contactType.ContactTypeID : 0,
                IsActive = true
            };
            await _businessEntityContactRepository.CreateAsync(businessEntityContact, cancellationToken);

            #endregion Company Details


            #region Localization

            if (companyLocalization != null)
            {
                Company company = new();
                company.BusinessEntityID = businessEntity.BusinessEntityID;
                company.Website = companyDetails?.CompanyWebsite;
                company.DateFormat = companyLocalization?.DateFormat;
                company.TimeFormat = companyLocalization?.TimeFormat;
                company.TimeZone = companyLocalization?.TimeZone;
                company.MeasurementTypeId = companyLocalization.MeasurementTypeId;
                company.CurrencyCode = companyLocalization?.Currency;
                company.LanguageCode = companyLocalization?.Language;
                company.IncludeCompanyLogo = companyLocalization.IncludeCompanyLogo;
                company.CompanyLogo = companyLocalization?.CompanyLogo;
                company.ThemeColor = companyLocalization?.ThemeColor;
                company.EmailAddress = companyDetails?.CompanyEmail;
                company.IsActive = companyDetails?.Status ?? true;
                await _companyRepository.CreateAsync(company, cancellationToken);
            }
            #endregion Localization


            #region Counters
            if (companyDetails?.CompanyCounters?.Count > 0)
            {
                foreach (var counterId in companyDetails.CompanyCounters)
                {
                    BusinessEntityCounter businessEntityCounter = new()
                    {
                        BusinessEntityId = businessEntity.BusinessEntityID,
                        CounterCategoryId = counterId.CounterCategoryId,
                        CounterValue = counterId.CounterValue,
                        IsActive = true
                    };
                    await _businessEntityCounterRepository.CreateAsync(businessEntityCounter, cancellationToken);
                }
            }

            #endregion Counters


            #region Terms 
            if (companyDetails != null && request.createCompanyVM.companyBrandVM?.QuoteFooterId > 0)
            {
                BusinessEntityTerm QuoteFooter = new()
                {
                    BusinessEntityID = businessEntity.BusinessEntityID,
                    TermID = request.createCompanyVM.companyBrandVM.QuoteFooterId,
                    IsActive = true
                };
                await _businessEntityTermRepository.CreateAsync(QuoteFooter, cancellationToken);
            }
            if (companyDetails != null && request.createCompanyVM.companyBrandVM?.QuoteTermsConditionsId > 0)
            {
                BusinessEntityTerm QuoteTermsConditions = new()
                {
                    BusinessEntityID = businessEntity.BusinessEntityID,
                    TermID = request.createCompanyVM.companyBrandVM.QuoteTermsConditionsId,
                    IsActive = true
                };
                await _businessEntityTermRepository.CreateAsync(QuoteTermsConditions, cancellationToken);
            }
            #endregion

            _logger.LogInformation($"CreateCompanyCommandHandler END");

            return new CreateCompanyResponseVM<int>(businessEntity.BusinessEntityID, IsSuccessful: true);
        }
        return new CreateCompanyResponseVM<int>(ErrorMessage: ErrorMessage.Company_SAVE_ERROR);
    }
}
