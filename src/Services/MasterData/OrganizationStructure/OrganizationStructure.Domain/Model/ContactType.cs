using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class ContactType : MasterBaseEntity
    {
        [Key]
        public int ContactTypeID { get; set; } 
        public string? ContactTypeName { get; set; } 
    }
}
