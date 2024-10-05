using Microsoft.EntityFrameworkCore;

namespace IAC.Domain.ViewModel.Role
{
	public class GetAllUserListFilterResponseVM
	{
		public GetAllUserListFilterResponseVM()
		{
		}
		public int UserId { get; set; }
		public string? UserName { get; set; }
		public string? UserRole { get; set; }
		public bool IsActive { get; set; }
	}
}
