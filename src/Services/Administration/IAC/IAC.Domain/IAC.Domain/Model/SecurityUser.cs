using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.Model
{
	public class SecurityUser : BaseEntity
	{
		[Key]
		public int UserId { get; set; }
		public string? LoginEmail { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public bool Status { get; set; }
		public int RoleID { get; set; }
		public int MaximumATOMDevices { get; set; }
	}
}
