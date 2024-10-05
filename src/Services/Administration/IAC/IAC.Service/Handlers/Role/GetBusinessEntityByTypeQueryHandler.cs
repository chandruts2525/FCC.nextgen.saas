using IAC.Domain.ViewModel.Role;
using IAC.Service.Queries.Role;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationStructure.Domain.Model;
using Softura.EntityFrameworkCore.Abstractions;

namespace IAC.Service.Handlers.Role
{
	public class GetBusinessEntityByTypeQueryHandler : IRequestHandler<GetBusinessEntityByTypeQuery, List<GetBusinessEntityByTypeResponseVM>>
    {
        private readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
        private readonly IRepository<BusinessEntity> _businessEntityRepository;

        private readonly ILogger<GetBusinessEntityByTypeQueryHandler> _logger;
        public GetBusinessEntityByTypeQueryHandler(IRepository<BusinessEntityType> businessEntityTypeRepository, IRepository<BusinessEntity> businessEntityRepository, ILogger<GetBusinessEntityByTypeQueryHandler> logger)
        {
            _businessEntityTypeRepository = businessEntityTypeRepository;
            _businessEntityRepository = businessEntityRepository;
            _logger = logger;
        }
        public async Task<List<GetBusinessEntityByTypeResponseVM>> Handle(GetBusinessEntityByTypeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start GetBusinessEntityByTypeQueryHandler, Parameters: BusinessEntityID: {request.getBusinessEntityByTypeResponseVM.BusinessEntityID}",request.getBusinessEntityByTypeResponseVM.BusinessEntityID);

            if (request.getBusinessEntityByTypeResponseVM.BusinessEntityTypeID != 0)
            {
                var result = await (from bet in _businessEntityTypeRepository.Get()
                                    join be in _businessEntityRepository.Get() on bet.BusinessEntityTypeId equals be.BusinessEntityTypeId
                                    where be.BusinessEntityTypeId == request.getBusinessEntityByTypeResponseVM.BusinessEntityTypeID
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
            else
            {
                return new List<GetBusinessEntityByTypeResponseVM>();
            }
        }
    }
}
