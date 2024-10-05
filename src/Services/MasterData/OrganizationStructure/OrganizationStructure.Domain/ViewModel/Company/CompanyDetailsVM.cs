namespace OrganizationStructure.Domain.ViewModel.Company;
public class CompanyDetailsVM
{
    public int BuisnessEntityId { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyEmail { get; set; }
    public string? CompanyPhone { get; set; }
    public string? CountryCode { get; set; }
    public string? CompanyWebsite { get; set; }
    public string? CodeName { get; set; }
    public bool Status { get; set; }
    public CompanyAddress? BusinessAddress { get; set; }
    public CompanyAddress? MailingAddress { get; set; }
    public List<DomainIdentifier>? DomainIdentifier { get; set; }
    public ContactInformation? contactInformation { get; set; }
    public List<CompanyCounters>? CompanyCounters { get; set; }

}
public class DomainIdentifier
{
    public string Identifier { get; set; }
}
public class CompanyCounters
{
    public int CounterCategoryId { get; set; }
    public int CounterValue { get; set; }

}
public class CompanyAddress
{
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Zip { get; set; }
}
public class ContactInformation
{
    public string? ContactName { get; set; }
    public string? EmailAddress { get; set; }
    public string? CountryCode { get; set; }
    public string? PhoneNumber { get; set; }
}
