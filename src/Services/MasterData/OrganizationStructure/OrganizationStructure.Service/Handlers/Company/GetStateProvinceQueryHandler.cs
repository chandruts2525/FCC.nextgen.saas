using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.Queries.Company;
using OrganizationStructure.Service.ViewModel.Company;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace OrganizationStructure.Service.Handlers.Company
{
    public class GetStateProvinceQueryHandler : IRequestHandler<GetStateProvinceByCountryCodeQuery, List<StateProvinceByCountryCodeVM>>
    {
        private readonly IRepository<StateProvince> _stateProvinceRepository;
        private readonly ILogger<GetStateProvinceQueryHandler> _logger;
        public GetStateProvinceQueryHandler(IRepository<StateProvince> stateProvinceRepository, ILogger<GetStateProvinceQueryHandler> logger)
        {
            _stateProvinceRepository = stateProvinceRepository;
            _logger = logger;
        }

        async Task<List<StateProvinceByCountryCodeVM>> IRequestHandler<GetStateProvinceByCountryCodeQuery, List<StateProvinceByCountryCodeVM>>.Handle(GetStateProvinceByCountryCodeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stateProvinceByCountryCodeList = new List<StateProvinceByCountryCodeVM>();
                if (!string.IsNullOrEmpty(request.CountryCode))
                {

                    _logger.LogInformation($"Start GetStateProvinceQueryHandler");
                    stateProvinceByCountryCodeList = await (from PR in _stateProvinceRepository.Get()
                                                            where PR.CountryCode == request.CountryCode
                                                            select new StateProvinceByCountryCodeVM
                                                            {
                                                                StateProvinceId = PR.StateProvinceId,
                                                                StateProvinceCode = PR.StateProvinceCode,
                                                                CountryCode = PR.CountryCode,
                                                                StateProvinceName = PR.StateProvinceName
                                                            }).ToListAsync(cancellationToken: cancellationToken);
                }
                _logger.LogInformation($"END GetCountryQueryHandler");
                return stateProvinceByCountryCodeList;
            }
            catch (Exception)
            {
                _logger.LogError($"Error in CompanyController.GetStateProvinceQueryHandler");

                throw;
            }

        }
    }
}
