namespace OrganizationStructure.Service.ViewModel.Yard
{
    public class AddressVM
    {
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public int StateProvinceId { get; set; }
        public string? StateProvinceName { get; set; }
        public string? StateProvinceCode { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public string? PostalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhoneCountryCode { get; set; }
    }
}
