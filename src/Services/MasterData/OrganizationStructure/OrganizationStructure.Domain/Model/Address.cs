using FCC.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationStructure.Domain.Model
{
    public class Address : BaseEntity
    {
        [Key]
        public int AddressID { get; set; } 
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public int StateProvinceId { get; set; }
        public string? PostalCode { get; set; }
    }
}
