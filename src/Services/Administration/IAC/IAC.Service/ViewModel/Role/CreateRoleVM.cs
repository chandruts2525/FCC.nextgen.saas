namespace IAC.Service.ViewModel.Role
{
	public class CreateRoleVM
    {
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? MappedUser { get; set; }
        public List<RoleAttachment>? Attachments { get; set; }
    }

    public class RoleAttachment
    {
        public string? FileName { get; set; }
        public string? FileURI { get; set; }
    }
}
