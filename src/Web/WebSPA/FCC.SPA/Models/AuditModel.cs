namespace FCC.SPA.Models
{
    public class AuditModel
    {
        public string? Area { get; set; }
        public string? ControllerName { get; set; }
        public string? Action { get; set; }
        public int RoleId { get; set; }
        public string IPAddress { get; set; }
        public bool IsFirstLogin { get; set; }
        public DateTime LoggedInAt { get; set; }
        public DateTime? LoggedOutAt { get; set; }
        public string LoginStatus { get; set; }
        public string SessionId { get; set; }
        public string? UserId { get; set; }
    }
}
