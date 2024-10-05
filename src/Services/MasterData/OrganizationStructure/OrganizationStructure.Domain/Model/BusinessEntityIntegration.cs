using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class BusinessEntityIntegration : BaseEntity
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public int ExternalIntegrationMetaDataID { get; set; }  
        public string? ParameterValue { get; set; } 
    }
}
