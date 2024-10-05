using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class CounterCategory : BaseEntity
    {
        [Key]
        public int CounterCategoryID { get; set; } 
        public string? CounterCategoryName { get; set; }
    }
}
