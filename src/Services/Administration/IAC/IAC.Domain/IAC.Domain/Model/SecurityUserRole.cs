using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.Model
{
	public class SecurityUserRole :BaseEntity
	{
		[Key]
        public int UserRoleId { get; set; }
		public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
