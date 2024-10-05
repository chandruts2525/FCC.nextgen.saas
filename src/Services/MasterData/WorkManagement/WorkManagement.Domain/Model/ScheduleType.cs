using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.Model
{
    public class ScheduleType : MasterBaseEntity
    {
        [Key]
        public int ScheduleTypeId { get; set; }
        public string? ScheduleTypeName { get; set; }
        public string? ScheduleTypeCode { get; set; }
        public int? UnitComponentsID { get; set; }
        public bool Schedulable { get; set; }
        public bool GroundBearingPresure { get; set; }
    }
}
