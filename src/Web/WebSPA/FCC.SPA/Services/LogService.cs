using FCC.SPA.Abstractions;
using FCC.SPA.Models;

namespace FCC.SPA.Services
{
    public class LogService : ILogService
    {
        private readonly IApiService _apiService;
        public LogService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<bool> SaveAuditLog(AuditModel auditModel)
        {
            bool res = false;

            InputParameters input = new InputParameters()
            {
                endpoint = "logs/saveAuditAsync",
                apiType = "",
                apiMethod = "Post",
                payload = auditModel
            };

            RestDtocs result = await _apiService.ExecuteRequestAsync(input);

            if (result != null)
            {
                res = true;
            }

            return res;
        }
    }
}
