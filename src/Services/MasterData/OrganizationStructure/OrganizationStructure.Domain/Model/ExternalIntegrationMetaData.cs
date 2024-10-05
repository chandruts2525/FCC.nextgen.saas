using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class ExternalIntegrationMetaData : BaseEntity
    {
        [Key]
        public int ExternalIntegrationMetaDataID { get; set; } 
        public int ExternalIntegrationVendorID { get; set; } 
        public string? MetaDataAttribute { get; set; } 
    }
}
