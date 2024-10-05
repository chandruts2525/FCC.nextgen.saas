namespace IAC.Domain.ViewModel.SecurityUser
{
    public class GetAllSecurityUser
    {
        public GetAllSecurityUser()
        {

        }
        public string? FirstNameFilter { get; set; }
        public string? LastNameFilter { get; set; }
        public string? LoginEmailFilter { get; set; }
        public string? StatusFilter { get; set; }
        public int? MaximumATOMDevicesFilter { get; set; }
        public string? SearchFilter { get; set; }
        public string? EmployeeFilter { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? OrderBy { get; set; }
        public string? SortOrderBy { get; set; }
    }
}
