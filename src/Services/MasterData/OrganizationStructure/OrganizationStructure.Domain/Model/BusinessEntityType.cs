using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class BusinessEntityType: BaseEntity
	{
		[Key]
		public int BusinessEntityTypeId { get; set; }
		public int BusinessEntityCategoryId { get; set; }

		public string? BusinessEntityTypeName { get; set; }
	}
}
