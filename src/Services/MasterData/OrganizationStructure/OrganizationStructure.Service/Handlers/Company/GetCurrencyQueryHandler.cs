using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.Queries.Company;
using OrganizationStructure.Service.ViewModel.Company;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace OrganizationStructure.Service.Handlers.Company
{
    internal class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, List<CurrencyVM>>
    {
        private readonly IRepository<Currency> _currencyRepository;
        private readonly ILogger<GetCurrencyQueryHandler> _logger;
        public GetCurrencyQueryHandler(IRepository<Currency> countryRepository, ILogger<GetCurrencyQueryHandler> logger)
        {
            _currencyRepository = countryRepository;
            _logger = logger;
        }
        async Task<List<CurrencyVM>> IRequestHandler<GetCurrencyQuery, List<CurrencyVM>>.Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start GetCurrencyQueryHandler");
                var result = await (from CR in _currencyRepository.Get()
                                    select new CurrencyVM
                                    {
                                        CurrencyCode = CR.CurrencyCode,
                                        CurrencyName = CR.CurrencyName,
                                    }).ToListAsync();
                _logger.LogInformation($"END GetCurrencyQueryHandler");
                if (result != null)
                    return result;
                else
                    return new List<CurrencyVM>();
            }
            catch (Exception)
            {
                _logger.LogError($"Error in CompanyController.GetCurrencyQueryHandler");

                throw;
            }

        }
    }
}
