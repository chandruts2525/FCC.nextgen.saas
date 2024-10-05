using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class CompanyDomain : MasterBaseEntity
    {
        [Key]
        public int BusinessEntityID { get; set; }
        public string? DomainIdentifier { get; set; } 
    }
}
