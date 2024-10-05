using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.Queries.Company;
using OrganizationStructure.Service.ViewModel.Company;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace OrganizationStructure.Service.Handlers.Company
{
    public class GetMeasurementTypeQueryHandler : IRequestHandler<GetMeasurementTypeQuery, List<MeasurementTypeVM>>
    {
        private readonly IRepository<MeasurementType> _measurementTypeRepository;
        private readonly ILogger<GetMeasurementTypeQueryHandler> _logger;
        public GetMeasurementTypeQueryHandler(IRepository<MeasurementType> countryRepository, ILogger<GetMeasurementTypeQueryHandler> logger)
        {
            _measurementTypeRepository = countryRepository;
            _logger = logger;
        }
        async Task<List<MeasurementTypeVM>> IRequestHandler<GetMeasurementTypeQuery, List<MeasurementTypeVM>>.Handle(GetMeasurementTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start GetCountryQueryHandler");
                var result = await (from MT in _measurementTypeRepository.Get()
                                    select new MeasurementTypeVM
                                    {
                                        MeasurementTypeID = MT.MeasurementTypeID,
                                        MeasurementTypeName = MT.MeasurementTypeName,
                                    }).ToListAsync();
                _logger.LogInformation($"END GetCountryQueryHandler");
                if (result != null)
                    return result;
                else
                    return new List<MeasurementTypeVM>();
            }
            catch (Exception)
            {
                _logger.LogError($"Error in CompanyController.GetCompanyQueryHandler");

                throw;
            }

        }
    }
}
