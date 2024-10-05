using WorkManagement.Service.Queries.QuoteFooter;
using WorkManagement.Service.ViewModel.QuoteFooters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;
using OrganizationStructure.Domain.Model;

namespace WorkManagement.Service.Handlers.QuoteFooter
{
	public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, List<BusinessEntityVM>>
    {
        private readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
        private readonly IRepository<BusinessEntity> _businessEntityRepository;
        private readonly ILogger<GetCompanyQueryHandler> _logger;
        public GetCompanyQueryHandler(IRepository<BusinessEntityType> businessEntityTypeRepository, IRepository<BusinessEntity> businessEntityRepository, ILogger<GetCompanyQueryHandler> logger)
        {
            _businessEntityTypeRepository = businessEntityTypeRepository;
            _businessEntityRepository = businessEntityRepository;
            _logger = logger;
        }
        async Task<List<BusinessEntityVM>> IRequestHandler<GetCompanyQuery, List<BusinessEntityVM>>.Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start GetCompanyQueryHandler");
                var result = await (from BE in _businessEntityRepository.Get()
                                    join BET in _businessEntityTypeRepository.Get() on BE.BusinessEntityTypeId equals BET.BusinessEntityTypeId
                                    where BET.BusinessEntityTypeName == "Company"
                                    select new BusinessEntityVM
                                    {
                                        BusinessEntityId = BE.BusinessEntityID,
                                        BusinessEntityName = BE.BusinessEntityName,
                                    }).ToListAsync(cancellationToken);
                _logger.LogInformation($"END GetCompanyQueryHandler");
                if (result != null)
                    return result;
                else
                    return new List<BusinessEntityVM>();
            }
            catch (Exception)
            {
                _logger.LogError($"Error in QuoteFooterCOntroller.GetCompanyQueryHandler");

                throw;
            }
            
        }
    }
}
