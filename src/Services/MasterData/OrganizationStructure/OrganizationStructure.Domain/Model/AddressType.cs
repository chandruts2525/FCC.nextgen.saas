using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class AddressType : MasterBaseEntity
    {
        [Key]
        public int AddressTypeId { get; set; } 
        public string? AddressTypeName { get; set; } 
    }
}
