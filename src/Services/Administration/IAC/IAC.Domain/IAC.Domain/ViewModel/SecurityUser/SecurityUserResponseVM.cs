using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.ViewModel.SecurityUser;

[Keyless]
public class SecurityUserResponseVM
{
	public int Count { get; set; }
	public List<SecurityUserResponse>? SecurityUserResponse { get; set; }
}

