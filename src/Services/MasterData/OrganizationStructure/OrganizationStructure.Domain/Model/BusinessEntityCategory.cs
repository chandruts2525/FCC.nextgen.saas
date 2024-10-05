using FCC.Data;

namespace OrganizationStructure.Domain.Model
{
    public class BusinessEntityCategory:BaseEntity
    {
        public int BusinessEntityCategoryId { get; set; }
        public string? BusinessEntityCategoryName { get; set; }

    }
}
