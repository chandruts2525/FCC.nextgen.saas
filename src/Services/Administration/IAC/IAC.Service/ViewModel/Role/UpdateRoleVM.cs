namespace IAC.Service.ViewModel.Role
{
	public class UpdateRoleVM
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? MappedUser { get; set; }
        public string? UnMappedUser { get; set; }
        public List<RoleAttachment>? Attachments { get; set; }
    }
}
