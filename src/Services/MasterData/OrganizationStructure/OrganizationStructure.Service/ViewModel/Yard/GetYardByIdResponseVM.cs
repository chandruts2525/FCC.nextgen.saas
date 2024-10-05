using FCC.Data;
using OrganizationStructure.Domain.Model;
using System.ComponentModel.DataAnnotations;
using WorkManagement.Domain.Model;

namespace OrganizationStructure.Service.ViewModel.Yard
{
    public class GetYardByIdResponseVM
    {
        public int BusinessEntityId { get; set; }
        public int BusinessEntityTypeId { get; set; }
        public string? BusinessEntityName { get; set; }
        public string? BusinessEntityCode { get; set; }
        public string? GLDistributionCode { get; set; }
        public bool Status { get; set; }
        public int HierarchyId { get; set; }
        public int ParentBusinessEntityId { get; set; }
        public int GeneralLedgerId { get; set; }
        public string? GLDescription { get; set; }
        public int BusinessEntityTermId { get; set; }
        public List<AddressVM>? Addresses { get; set; }
        public List<TermsVM>? Term { get; set; }
        public List<BusinessEntityCounterVM>? BusinessEntityCounters { get; set; }
    }
}
