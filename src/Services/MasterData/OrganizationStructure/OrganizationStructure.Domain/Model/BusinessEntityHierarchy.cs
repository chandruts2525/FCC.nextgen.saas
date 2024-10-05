using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
    public class BusinessEntityHierarchy : MasterBaseEntity
    {
        [Key]
        public int HierarchyId { get; set; }
        public int BusinessEntityID { get; set; }
        public int parentBusinessEntityId { get; set; }
    }
}
