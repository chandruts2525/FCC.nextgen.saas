namespace IAC.Service.ViewModel.User
{
    public class UpdateSecurityUserViewModel
    {
        public int UserId { get; set; }
        public string? LoginEmail { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool Status { get; set; }
        public int RoleID { get; set; }
        public int MaximumATOMDevices { get; set; }
        public string? OldBusinessEntityIDs { get; set; }
        public string? NewBusinessEntityIDs { get; set; }

    }
}
