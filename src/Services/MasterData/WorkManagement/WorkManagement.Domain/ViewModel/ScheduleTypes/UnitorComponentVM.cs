using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.ViewModel.ScheduleTypes
{
	public class UnitorComponentVM
    {
        public UnitorComponentVM() { }
        [Key]
        public int UnitComponentsID { get; set; }
        public string? UnitOrComponent { get; set; }
    }
}