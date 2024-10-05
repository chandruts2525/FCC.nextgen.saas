using IAC.Domain.Model;
using IAC.Domain.ViewModel.Role;
using IAC.Service.Queries.Role;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationStructure.Domain.Model;
using Softura.EntityFrameworkCore.Abstractions;

namespace IAC.Service.Handlers.Role
{
	public class GetBusinessTypesQueryHandler : IRequestHandler<GetBusinessTypesQuery, List<GetBusinessEntityByTypeResponseVM>>
    {
        private readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
        private readonly IRepository<BusinessEntity> _businessEntityRepository;

        private readonly ILogger<GetBusinessTypesQueryHandler> _logger;
        public GetBusinessTypesQueryHandler(IRepository<BusinessEntityType> businessEntityTypeRepository, IRepository<BusinessEntity> businessEntityRepository, ILogger<GetBusinessTypesQueryHandler> logger)
        {
            _businessEntityTypeRepository = businessEntityTypeRepository;
            _businessEntityRepository = businessEntityRepository;
            _logger = logger;
        }
        async Task<List<GetBusinessEntityByTypeResponseVM>> IRequestHandler<GetBusinessTypesQuery, List<GetBusinessEntityByTypeResponseVM>>.Handle(GetBusinessTypesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetBusinessTypesQueryHandler");

            var result = await (from bet in _businessEntityTypeRepository.Get()
                                join be in _businessEntityRepository.Get() on bet.BusinessEntityTypeId equals be.BusinessEntityTypeId
                                where bet.BusinessEntityTypeName == "DEPARTMENT" || bet.BusinessEntityTypeName == "YARD" || bet.BusinessEntityTypeName == "COMPANY"
                                select new GetBusinessEntityByTypeResponseVM
                                {
                                    BusinessEntityTypeID = bet.BusinessEntityTypeId,
                                    BusinessEntityTypeName = bet.BusinessEntityTypeName,
                                    BusinessEntityID = be.BusinessEntityID,
                                    BusinessEntityName = be.BusinessEntityName,
                                }).ToListAsync(cancellationToken);

            _logger.LogInformation($"GetBusinessEntityByTypeQueryHandler End");

            if (result != null)
                return result;
            else
                return new List<GetBusinessEntityByTypeResponseVM>();
        }
    }
}
