using OrganizationManagementService.Service.ViewModel.Yard;

namespace OrganizationStructure.Service.ViewModel.Yard
{
    public class UpdateYardVM
    {
        public int BusinessEntityId { get; set; }
        public int BusinessEntityTypeId { get; set; }
        public string? BusinessEntityName { get; set; }
        public string? BusinessEntityCode { get; set; }
        public string? GLDistributionCode { get; set; }
        public bool Status { get; set; }
        public int ParentBusinessEntityId { get; set; }
        public int GeneralLedgerId { get; set; }
        public List<AddressVM>? Addresses { get; set; }
        public List<BusinessEntityTermVM>? BusinessEntityTerms { get; set; }
        public List<BusinessEntityCounterVM>? BusinessEntityCounters { get; set; }
    }
}
