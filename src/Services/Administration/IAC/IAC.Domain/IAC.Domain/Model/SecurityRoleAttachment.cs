using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.Model
{
	public class SecurityRoleAttachment :BaseEntity
	{
        [Key]
        public int RoleUserMappingID { get; set; }
		public int RoleId { get; set; }
		public string? Url { get; set; }
        public string? FileName { get; set; }

    }
}
