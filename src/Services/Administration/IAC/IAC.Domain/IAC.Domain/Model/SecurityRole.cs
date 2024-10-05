using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.Model
{
	public class SecurityRole : BaseEntity
	{
		[Key]
		public int RoleId { get; set; }
		public string? RoleName { get; set; }
	}
}
