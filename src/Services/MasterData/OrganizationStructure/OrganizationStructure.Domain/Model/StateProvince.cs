using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
    public class StateProvince : BaseEntity
    {
        [Key]
        public int StateProvinceId { get; set; }
        public string? StateProvinceCode { get; set; }
        public string? CountryCode { get; set; }
        public string? StateProvinceName { get; set; }
    }
}
