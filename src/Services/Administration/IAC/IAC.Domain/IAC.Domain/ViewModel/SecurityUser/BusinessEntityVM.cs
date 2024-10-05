using System.ComponentModel.DataAnnotations;
namespace IAC.Domain.ViewModel.SecurityUser;
public class BusinessEntityVM
{
    [Key]
    public int BusinessEntityId { get; set; }
    public string? BusinessEntityName { get; set; }
    public string? BusinessEntityTypeName { get; set; }
} 
