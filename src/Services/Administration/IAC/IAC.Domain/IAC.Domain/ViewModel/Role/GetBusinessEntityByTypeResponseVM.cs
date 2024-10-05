using System.ComponentModel.DataAnnotations;

namespace IAC.Domain.ViewModel.Role
{
    public class GetBusinessEntityByTypeResponseVM
	{
        [Key]
        public int BusinessEntityID { get; set; }
        public int BusinessEntityTypeID { get; set; }
        public string? BusinessEntityTypeName { get; set; }
        public string? BusinessEntityName { get; set; }
    }
}
