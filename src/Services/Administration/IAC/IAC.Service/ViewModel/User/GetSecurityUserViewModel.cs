namespace IAC.Service.ViewModel.User
{
    public class GetSecurityUserViewModel
    {
        public GetSecurityUserViewModel() { }
        public int UserId { get; set; }

    }
    public class GetSecurityUserPermisionViewModel
    {
        public GetSecurityUserPermisionViewModel() { }
        public int UserId { get; set; }
        public string? BusinessEntityTypeName { get; set; }

    }
}
