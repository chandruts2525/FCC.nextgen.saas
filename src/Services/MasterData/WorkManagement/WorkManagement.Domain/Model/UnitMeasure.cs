using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.Model
{
    public class UnitMeasure : MasterBaseEntity
    {
        [Key]
        public int UnitMeasureId { get; set; }
        public string UnitMeasureCode { get; set; } = null!;
        public int? UnitMeasureTypeId { get; set; }
        public string? UnitMeasureDisplayValue { get; set; }
        public int? ConversionFactor { get; set; }

    }
}
