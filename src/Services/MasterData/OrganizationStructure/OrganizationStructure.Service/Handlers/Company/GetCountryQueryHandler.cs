using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.Queries.Company;
using OrganizationStructure.Service.ViewModel.Company;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace OrganizationStructure.Service.Handlers.Company
{
    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, List<CountryVM>>
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly ILogger<GetCountryQueryHandler> _logger;
        public GetCountryQueryHandler(IRepository<Country> countryRepository, ILogger<GetCountryQueryHandler> logger)
        {
            _countryRepository = countryRepository;
            _logger = logger;
        }
        async Task<List<CountryVM>> IRequestHandler<GetCountryQuery, List<CountryVM>>.Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start GetCountryQueryHandler");
                var result = await (from CR in _countryRepository.Get()
                                    select new CountryVM
                                    {
                                        CountryCode = CR.CountryCode,
                                        CountryName = CR.CountryName,
                                    }).ToListAsync();
                _logger.LogInformation($"END GetCountryQueryHandler");
                if (result != null)
                    return result;
                else
                    return new List<CountryVM>();
            }
            catch (Exception)
            {
                _logger.LogError($"Error in CompanyController.GetCompanyQueryHandler");

                throw;
            }

        }
    }
}
