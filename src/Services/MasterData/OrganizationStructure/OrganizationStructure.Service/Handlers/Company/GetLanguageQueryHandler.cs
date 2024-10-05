using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.Queries.Company;
using OrganizationStructure.Service.ViewModel.Company;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace OrganizationStructure.Service.Handlers.Company
{
    internal class GetLanguageQueryHandler : IRequestHandler<GetLanguageQuery, List<LanguageVM>>
    {
        private readonly IRepository<Language> _languageRepository;
        private readonly ILogger<GetLanguageQueryHandler> _logger;
        public GetLanguageQueryHandler(IRepository<Language> countryRepository, ILogger<GetLanguageQueryHandler> logger)
        {
            _languageRepository = countryRepository;
            _logger = logger;
        }
        async Task<List<LanguageVM>> IRequestHandler<GetLanguageQuery, List<LanguageVM>>.Handle(GetLanguageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start GetLanguageQueryHandler");
                var result = await (from LN in _languageRepository.Get()
                                    select new LanguageVM
                                    {
                                        LanguageCode = LN.LanguageCode,
                                        LanguageName = LN.LanguageName,
                                    }).ToListAsync();
                _logger.LogInformation($"END GetLanguageQueryHandler");
                if (result != null)
                    return result;
                else
                    return new List<LanguageVM>();
            }
            catch (Exception)
            {
                _logger.LogError($"Error in CompanyController.GetLanguageQueryHandler");

                throw;
            }

        }
    }
}
