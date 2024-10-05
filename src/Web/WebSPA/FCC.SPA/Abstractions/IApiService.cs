using FCC.SPA.Models;

namespace FCC.SPA.Abstractions
{
    public interface IApiService
    {
        Task<RestDtocs> ExecuteRequestAsync(InputParameters param);
    }
}
