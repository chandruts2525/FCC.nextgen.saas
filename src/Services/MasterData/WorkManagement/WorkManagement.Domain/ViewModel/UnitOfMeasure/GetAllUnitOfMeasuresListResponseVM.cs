using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.ViewModel.UnitOfMeasure
{
    public class GetAllUnitOfMeasuresListResponseVM
    {
        [Key]
        public int UnitMeasureId { get; set; }
        public string UnitMeasureCode { get; set; } = null!;
        public string UnitMeasureDisplayValue { get; set; } = null!;
        public int? UnitMeasureTypeId { get; set; }
        public string? UnitMeasureTypeDescription { get; set; }
        public int? ConversionFactor { get; set; }
        public string? IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? CreatedDateUTC { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedDateUTC { get; set; }
    }
}
