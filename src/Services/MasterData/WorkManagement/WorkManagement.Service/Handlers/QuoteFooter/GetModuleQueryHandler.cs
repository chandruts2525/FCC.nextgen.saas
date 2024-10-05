using WorkManagement.Domain.Model;
using WorkManagement.Service.Queries.QuoteFooter;
using WorkManagement.Service.ViewModel.QuoteFooters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.QuoteFooter
{
    public class GetModuleQueryHandler : IRequestHandler<GetModuleQuery, List<ModulesVM>>
    {
        private readonly IRepository<Modules> _ModuleRepository;
        private readonly ILogger<GetModuleQueryHandler> _logger;
        public GetModuleQueryHandler(IRepository<Modules> ModuleRepository,  ILogger<GetModuleQueryHandler> logger)
        {
            _ModuleRepository = ModuleRepository;
            _logger = logger;
        }
        async Task<List<ModulesVM>> IRequestHandler<GetModuleQuery, List<ModulesVM>>.Handle(GetModuleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start GetModuleQueryHandler");
                var result = await _ModuleRepository.Get().Where(x => !x.IsDeleted)
                    .Select(s => new ModulesVM
                    {
                        ModuleID = s.ModuleID,
                        ModuleName = s.ModuleName
                    }).ToListAsync(cancellationToken);
                _logger.LogInformation($"END GetModuleQueryHandler");
                if(result != null)
                    return result;
                else
                    return new List<ModulesVM>();
            }
            catch (Exception)
            {
                _logger.LogError("Error in QuoteFooterController.GetModuleQueryHandler");
                throw;
            }
            
        }
    }
}
