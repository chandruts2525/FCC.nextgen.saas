using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationManagementService.Service.Queries.Yard;
using OrganizationStructure.Service.ViewModel.Yard;
using Softura.EntityFrameworkCore.Abstractions;
using WorkManagement.Domain.Model;

namespace OrganizationStructure.Service.Handler.Yards
{
    public class GetTermsFooterQueryHandler : IRequestHandler<GetTermsFooterQuery, List<TermsVM>>
    {
        private readonly IRepository<Terms> _termRepository;
        private readonly ILogger<GetTermsFooterQueryHandler> _logger;
        public GetTermsFooterQueryHandler(IRepository<Terms> termRepository, 
            ILogger<GetTermsFooterQueryHandler> logger)
        {
            _termRepository = termRepository;
            _logger = logger;
        }
        async Task<List<TermsVM>> IRequestHandler<GetTermsFooterQuery, List<TermsVM>>.Handle(GetTermsFooterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start GetGeneralLedgerQuery");
                var result = await (from t in _termRepository.Get()
                                    where t.IsActive
                                    select new TermsVM
                                    {
                                        TermID = t.TermID,
                                        TermTypeID = t.TermTypeID,
                                        TermName = t.TermName,
                                    }).ToListAsync(cancellationToken);
                _logger.LogInformation($"END GetGeneralLedgerQuery");
                if (result != null)
                    return result;
                else
                    return new List<TermsVM>();
            }
            catch (Exception)
            {
                _logger.LogError($"Error in YardController.GetGeneralLedgerQuery");

                throw;
            }
        }
    }
}
