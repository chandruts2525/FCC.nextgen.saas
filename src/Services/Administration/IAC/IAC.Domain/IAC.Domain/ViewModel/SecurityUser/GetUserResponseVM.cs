using System.ComponentModel.DataAnnotations;
namespace IAC.Domain.ViewModel.SecurityUser;
public class GetUserResponseVM
{
    [Key]
    public int UserId { get; set; }
    public string? LoginEmail { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? RoleID { get; set; }
    public string? RoleName { get; set; }
    public int MaximumATOMDevices { get; set; } 
    public bool Status { get; set; }
    public List<BusinessEntityVM>? Employee { get; set; }
    public List<BusinessEntityVM>? Companies { get; set; }
    public List<BusinessEntityVM>? Yards { get; set; }
    public List<BusinessEntityVM>? Departments { get; set; }
}
