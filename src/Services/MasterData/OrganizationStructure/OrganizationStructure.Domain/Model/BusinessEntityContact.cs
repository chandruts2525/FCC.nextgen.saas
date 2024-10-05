using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class BusinessEntityContact : BaseEntity
    {
        [Key]
        public int BusinessEntityID { get; set; }
        public int ContactTypeID { get; set; }
        public string? ContactName { get; set; } 
        public string? EmailAddress { get; set; }
        public string? CountryCode { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
