namespace FCC.Core.ViewModel.GridFilter
{
    public class ColumnFilter
    {
        public string Field { get; set; } = null!;
        public string Operator { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
