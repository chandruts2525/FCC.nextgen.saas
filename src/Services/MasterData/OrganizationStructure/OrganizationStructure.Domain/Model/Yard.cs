using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
    public class Yard : MasterBaseEntity
    {
        [Key]
        public int BusinessEntityId { get; set; }
    }
}
