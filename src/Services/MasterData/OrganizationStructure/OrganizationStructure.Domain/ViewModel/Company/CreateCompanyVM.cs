namespace OrganizationStructure.Domain.ViewModel.Company;
public class CreateCompanyVM
{
    public CompanyDetailsVM? companyDetailsVM { get; set; }
    public CompanyLocalizationVM? companyLocalizationVM { get; set; }
    public CompanyDocuSignVM? companyDocuSignVM { get; set; }
    public CompanyBrandVM? companyBrandVM { get; set; }
    public int ParentBusinessEntityId { get; set; }

}
