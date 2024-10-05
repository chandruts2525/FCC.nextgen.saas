using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.ViewModel.Role
{
	public class SearchRoleResponseVM
	{
		[Key]
		public int RoleId { get; set; }
		public string? RoleName { get; set; }
		public string? CreatedBy { get; set; }
		public bool? IsActive { get; set; }
		public int? AssignedUser { get; set; }
	}
}
