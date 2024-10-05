using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
    public class BusinessEntityCounter : MasterBaseEntity
    {
        public int BusinessEntityId { get; set; }
        public int CounterCategoryId { get; set; }
        public int CounterValue { get; set; }
    }
}
