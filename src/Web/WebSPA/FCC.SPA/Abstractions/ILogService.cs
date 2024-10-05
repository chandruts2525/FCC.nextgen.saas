using FCC.SPA.Models;

namespace FCC.SPA.Abstractions
{
    public interface ILogService
    {
        Task<bool> SaveAuditLog(AuditModel auditModel);
    }
}
