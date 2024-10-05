using Microsoft.EntityFrameworkCore;

namespace IAC.Domain.ViewModel.Role
{
	[Keyless]
    public class GetAllUserByBusinessEntityResponseVM
    {
		public int UserId { get; set; }
        public int? RoleId { get; set; }
		public string? UserName { get; set; }
        public string? UserRole { get; set; }
        public bool IsActive { get; set; }
		public int? BusinessEntityId { get; set; }
		public int? BusinessEntityTypeId { get; set; }
	}
}
