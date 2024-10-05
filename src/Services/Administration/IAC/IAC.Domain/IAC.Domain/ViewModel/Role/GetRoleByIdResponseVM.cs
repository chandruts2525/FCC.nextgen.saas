using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.ViewModel.Role
{
	[Keyless]
	public class GetRoleByIdResponseVM
	{
		public int RoleUserMappingID { get; set; }
		public string? RoleName { get; set; }
		public int RoleId { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public string? CreatedBy { get; set; }
		public string? ModifiedBy { get; set; }
		public int? UserId { get; set; }
		public string? UserName { get; set; }
		public string? UserRole { get; set; }
		public bool? IsActive { get; set; }
	}
}
