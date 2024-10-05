using FCC.Data;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model;

public class Company : MasterBaseEntity
{
    [Key]
    public int BusinessEntityID { get; set; }
    public int MeasurementTypeId { get; set; }
    public string? CurrencyCode { get; set; }
    public string? LanguageCode { get; set; }
    public string? DateFormat { get; set; }
    public string? TimeFormat { get; set; }
    public string? TimeZone { get; set; }
    public bool IncludeCompanyLogo { get; set; }
    public string? ThemeColor { get; set; }
    public string? EmailAddress { get; set; }
    public string? Website { get; set; }
    public string? CompanyLogo { get; set; }
}
