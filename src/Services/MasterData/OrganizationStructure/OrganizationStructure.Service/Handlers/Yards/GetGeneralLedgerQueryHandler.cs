using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationManagementService.Service.Queries.Yard;
using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.ViewModel.Yard;
using Softura.EntityFrameworkCore.Abstractions;

namespace OrganizationStructure.Service.Handler.Yards
{
    public class GetGeneralLedgerQueryHandler : IRequestHandler<GetGeneralLedgerQuery, List<GeneralLedgerVM>>
    {
        private readonly IRepository<GeneralLedger> _generalLedgerRepository;
        private readonly ILogger<GetGeneralLedgerQueryHandler> _logger;
        public GetGeneralLedgerQueryHandler(IRepository<GeneralLedger> generalLedgerRepository, 
            ILogger<GetGeneralLedgerQueryHandler> logger)
        {
            _generalLedgerRepository = generalLedgerRepository;
            _logger = logger;
        }
        async Task<List<GeneralLedgerVM>> IRequestHandler<GetGeneralLedgerQuery, List<GeneralLedgerVM>>.Handle(GetGeneralLedgerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start GetGeneralLedgerQuery");
                var result = await (from GL in _generalLedgerRepository.Get()
                                    where GL.IsActive 
                                    select new GeneralLedgerVM
                                    {
                                        GeneralLedgerId = GL.GeneralLedgerId,
                                        GeneralLedgerAccountCode = GL.GeneralLedgerAccountCode,
                                        Description = GL.Description,
                                    }).ToListAsync(cancellationToken);
                _logger.LogInformation($"END GetGeneralLedgerQuery");
                if (result != null)
                    return result;
                else
                    return new List<GeneralLedgerVM>();
            }
            catch (Exception)
            {
                _logger.LogError($"Error in YardController.GetGeneralLedgerQuery");

                throw;
            }
        }
    }
}
