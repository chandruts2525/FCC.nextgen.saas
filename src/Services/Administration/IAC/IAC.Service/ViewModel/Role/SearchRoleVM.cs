namespace IAC.Service.ViewModel.Role
{
    public class SearchRoleVM
    {
        public string? RoleName { get; set; }
        public int? UserCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? OrderBy { get; set; }
        public string? SortOrder { get; set; }
    }
}