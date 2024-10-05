using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.ViewModel.SecurityUser;

public class SecurityUserResponse
{
	[Key]
	public int UserID { get; set; }
	public string? LastName { get; set; }
	public string? FirstName { get; set; }
	public string? LoginEmail { get; set; }
	public bool Status { get; set; }
	public int? MaximumATOMDevices { get; set; }
	public string? EmployeeName { get; set; }
}
