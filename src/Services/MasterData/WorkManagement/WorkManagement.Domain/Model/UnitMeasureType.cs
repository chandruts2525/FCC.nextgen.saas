using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.Model
{
    public class UnitMeasureType : MasterBaseEntity
    {
        [Key]
        public int UnitMeasureTypeId { get; set; }
        public string UnitMeasureTypeDescription { get; set; } = null!;

    }
}
