using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class BusinessEntity : BaseEntity
    {
        [Key]
        public int BusinessEntityID { get; set; }
        public int BusinessEntityTypeId { get; set; }
        public string? BusinessEntityName { get; set; }
        public string? BusinessEntityCode { get; set; }
        public string? GLDistributionCode { get; set; }
        public string? Description { get; set; }
    }
}
