using FCC.Core.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationManagementService.Service.Queries.Yard;
using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.ViewModel.Yard;
using Softura.EntityFrameworkCore.Abstractions;

namespace OrganizationStructure.Service.Handler.Yards
{
    public class GetYardQueryHandler : IRequestHandler<GetYardQuery, List<GetYardResponseVM>>
    {
        private readonly IRepository<BusinessEntity> _businessEntityRepository;
        private readonly IRepository<BusinessEntityHierarchy> _businessEntityHierarchyRepository;
        private readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
        private readonly ILogger<GetYardQueryHandler> _logger;
        public GetYardQueryHandler(IRepository<BusinessEntityType> businessEntityTypeRepository,
            IRepository<BusinessEntity> businessEntityRepository,
            IRepository<BusinessEntityHierarchy> businessEntityHierarchyRepository,
            ILogger<GetYardQueryHandler> logger)
        {
            _businessEntityRepository = businessEntityRepository;
            _businessEntityTypeRepository = businessEntityTypeRepository;
            _businessEntityHierarchyRepository = businessEntityHierarchyRepository;
            _logger = logger;
        }
        async Task<List<GetYardResponseVM>> IRequestHandler<GetYardQuery, List<GetYardResponseVM>>.Handle(GetYardQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Start GetYardQueryHandler");
                var result = await (from beh in _businessEntityHierarchyRepository.Get()
                                    where beh.parentBusinessEntityId == request.ParentBusinessEntityId
&& beh.IsActive && !beh.IsDeleted
                                    join be in _businessEntityRepository.Get() on beh.BusinessEntityID equals be.BusinessEntityID
                                    where be.IsActive
                                    join bet in _businessEntityTypeRepository.Get(x => x.BusinessEntityTypeName == BuisnessEntityName.Yard.ToString())
                                    on be.BusinessEntityTypeId equals bet.BusinessEntityTypeId
                                    select new GetYardResponseVM
                                    {
                                        ParentBusinessEntityId = beh.parentBusinessEntityId,
                                        BusinessEntityId = be.BusinessEntityID,
                                        BusinessEntityName = be.BusinessEntityName,
                                    }).ToListAsync(cancellationToken);
                _logger.LogInformation($"END GetYardQueryHandler");
                if (result != null)
                    return result;
                else
                    return new List<GetYardResponseVM>();
            }
            catch (Exception)
            {
                _logger.LogError($"Error in YardController.GetYardQueryHandler");

                throw;
            }
        }
    }
}
