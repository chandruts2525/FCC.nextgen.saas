using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class ExternalIntegrationVendor : BaseEntity
    {
        [Key]
        public int ExternalIntegrationVendorID { get; set; } 
        public string? VendorName { get; set; } 
    }
}
