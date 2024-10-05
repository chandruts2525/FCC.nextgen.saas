using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class PhoneNumberType : MasterBaseEntity
    {
        [Key] 
        public int PhoneNumberTypeId { get; set; }
        public string? PhoneNumberTypeName { get; set; }  
    }
}
