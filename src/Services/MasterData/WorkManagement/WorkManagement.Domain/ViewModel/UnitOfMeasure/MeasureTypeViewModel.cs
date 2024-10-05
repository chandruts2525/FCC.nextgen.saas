using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.ViewModel.UnitOfMeasure
{
    public class MeasureTypeViewModel
    {
        [Key]
        public int UnitMeasureTypeId { get; set; }
        public string UnitMeasureTypeDescription { get; set; } = null!;
    }
}
