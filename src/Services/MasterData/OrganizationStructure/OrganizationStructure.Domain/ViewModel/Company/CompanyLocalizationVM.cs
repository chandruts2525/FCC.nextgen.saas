namespace OrganizationStructure.Domain.ViewModel.Company;
public class CompanyLocalizationVM
{
    public string? Language { get; set; }
    public string? Currency { get; set; }
    public string? TimeZone { get; set; }
    public string? DateFormat { get; set; }
    public string? TimeFormat { get; set; }
    public int MeasurementTypeId { get; set; }
    public bool IncludeCompanyLogo { get; set; }
    public string? ThemeColor { get; set; }
    public string? CompanyLogo { get; set; }
}

