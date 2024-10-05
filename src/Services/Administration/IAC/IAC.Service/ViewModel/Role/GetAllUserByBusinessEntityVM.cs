namespace IAC.Service.ViewModel.Role
{
    public class GetAllUserByBusinessEntityVM
    {
        public GetAllUserByBusinessEntityVM()
        {
        }
        public string? BusinessEntityTypeIds { get; set; }
        public string? BusinessEntityIds { get; set; }
        public int? RoleId { get; set; }
    }
}