using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.Model
{
	public class ScheduleTypeComponents
    {
        [Key]
        public int UnitComponentsID { get; set; }
        public string? UnitOrComponent { get; set; }
    }
}
