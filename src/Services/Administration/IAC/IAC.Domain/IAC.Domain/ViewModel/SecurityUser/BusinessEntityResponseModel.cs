using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.ViewModel.SecurityUser
{
    public class BusinessEntityResponseModel
    {
        public BusinessEntityResponseModel() { }
        [Key]
        public int BusinessEntityID { get; set; }
        public string? BusinessEntityName { get; set; }
    }
}