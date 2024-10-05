using FCC.Core.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationManagementService.Service.Queries.Region;
using OrganizationManagementService.Service.ViewModel.Region;
using OrganizationStructure.Domain.Model;
using Softura.EntityFrameworkCore.Abstractions;

namespace OrganizationManagementService.Service.Handlers.Region
{
    public class GetAllRegionByParentIdQueryHandler : IRequestHandler<GetAllRegionByParentIdQuery, List<BusinessEntityVM>>
    {
        private readonly IRepository<BusinessEntity> _businessEntityRepository;
        private readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
        private readonly IRepository<BusinessEntityHierarchy> _businessEntityHierarchyRepository;
        private readonly ILogger<GetAllRegionByParentIdQueryHandler> _logger;

        public GetAllRegionByParentIdQueryHandler(IRepository<BusinessEntity> businessEntityRepository, IRepository<BusinessEntityType> businessEntityTypeRepository, IRepository<BusinessEntityHierarchy> businessEntityHierarchyRepository, ILogger<GetAllRegionByParentIdQueryHandler> logger)
        {
            _businessEntityHierarchyRepository = businessEntityHierarchyRepository;
            _businessEntityRepository = businessEntityRepository;
            _businessEntityTypeRepository = businessEntityTypeRepository;
            _logger = logger;
        }
        async Task<List<BusinessEntityVM>> IRequestHandler<GetAllRegionByParentIdQuery, List<BusinessEntityVM>>.Handle(GetAllRegionByParentIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetAllRegionByParentIdQueryHandler");

            var getAllRegion = await (from BEH in _businessEntityHierarchyRepository.Get().Where(x => x.parentBusinessEntityId == request.parentBusinessEntityId && !x.IsDeleted && x.IsActive)
                                      join BE in _businessEntityRepository.Get().Where(x => x.IsActive && !x.IsDeleted) on BEH.BusinessEntityID equals BE.BusinessEntityID
                                      join BET in _businessEntityTypeRepository.Get(x => x.BusinessEntityTypeName == BuisnessEntityName.Region.ToString())
                                      on BE.BusinessEntityTypeId equals BET.BusinessEntityTypeId
                                      select new BusinessEntityVM
                                      {
                                          BusinessEntityId = BEH.BusinessEntityID,
                                          BusinessEntityName = BE.BusinessEntityName
                                      }).ToListAsync();

            _logger.LogInformation($"End GetAllRegionByParentIdQueryHandler");

            return getAllRegion;
        }
    }
}
