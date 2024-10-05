namespace FCC.Core.ViewModel.GridFilter
{
    public class GridFilter
    {
        public List<ColumnFilter>? ColumnFilters { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? GlobalSearch { get; set; }
       
    }
}
