using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
    public class GeneralLedger : MasterBaseEntity
    {
        [Key]
        public int GeneralLedgerId { get; set; }
        public string? GeneralLedgerAccountCode { get; set; }
        public string? Description { get; set; }
        public string? Website { get; set; }
        public bool PayrollWeekEnding { get; set; }
        public bool ShouldSplitMultipleDays { get; set; }
    }
}
