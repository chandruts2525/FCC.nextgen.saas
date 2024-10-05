namespace WorkManagement.Domain.ViewModel.UnitOfMeasure
{
    public class UnitOfMeasureResponseVM
    {
        public int UnitMeasureId { get; set; }
        public string UnitMeasureCode { get; set; } = null!;
        public string UnitMeasureName { get; set; } = null!;
        public int? UnitMeasureTypeId { get; set; }
        public int? ConversionFactor { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string? SavedBy { get; set; }
        public string? Response { get; set; }
    }
}
