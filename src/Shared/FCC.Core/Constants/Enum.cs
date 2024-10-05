using System.ComponentModel;
namespace FCC.Core.Constants;

public enum BuisnessEntityName
{
    
    [Description("Employee")]
    Employee = 1,
    [Description("Company")]
    Company = 2,
    [Description("Yard")]
    Yard = 3,
    [Description("Department")]
    Department = 4,
    [Description("Region")]
    Region = 13,
    [Description("Organization")]
    Organization =5
}
public enum PhoneNumberTypes
{
    [Description("Business Phone")]
    BusinessPhone,
    [Description("Mailing Phone")]
    MailingPhone,
    [Description("Physical Phone")]
    PhysicalPhone,
    [Description("Company Phone")]
    CompanyPhone,
}
public enum AddressTypes
{
    [Description("Business Address")]
    BusinessAddress,
    [Description("Mailing Address")]
    MailingAddress, 
    [Description("Physical Address")]
    PhysicalAddress,
}