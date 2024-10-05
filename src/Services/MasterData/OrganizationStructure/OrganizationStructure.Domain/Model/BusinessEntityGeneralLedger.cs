using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
    public class BusinessEntityGeneralLedger : MasterBaseEntity
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public int GeneralLedgerId { get; set; }
    }
}
