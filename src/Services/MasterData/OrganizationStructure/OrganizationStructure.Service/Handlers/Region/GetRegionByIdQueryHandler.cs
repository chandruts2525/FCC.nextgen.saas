using MediatR;
using Microsoft.Extensions.Logging;
using OrganizationManagementService.Service.Queries.Region;
using OrganizationManagementService.Service.ViewModel.Region;
using Softura.EntityFrameworkCore.Abstractions;
using Microsoft.EntityFrameworkCore;
using OrganizationStructure.Domain.Model;
using FCC.Core.Constants;
using FCC.Core;

namespace OrganizationManagementService.Service.Handlers.Region
{
    public class GetRegionByIdQueryHandler : IRequestHandler<GetRegionByIdQuery, GetRegionByIdVM>
    {
        private readonly IRepository<BusinessEntity> _businessEntityRepository;
        private readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
        private readonly IRepository<BusinessEntityHierarchy> _businessEntityHierarchyRepository;
        private readonly ILogger<GetRegionByIdQueryHandler> _logger;

        public GetRegionByIdQueryHandler(IRepository<BusinessEntity> businessEntityRepository,IRepository<BusinessEntityType> businessEntityTypeRepository, IRepository<BusinessEntityHierarchy> businessEntityHierarchyRepository, ILogger<GetRegionByIdQueryHandler> logger)
        {
            _businessEntityRepository = businessEntityRepository;
            _businessEntityHierarchyRepository = businessEntityHierarchyRepository;
            _businessEntityTypeRepository = businessEntityTypeRepository;
            _logger = logger;
        }

        public async Task<GetRegionByIdVM> Handle(GetRegionByIdQuery request, CancellationToken cancellationToken)
        {
            GetRegionByIdVM region = new();
            _logger.LogInformation($"Start GetRegionByIdQueryHandler");

            var businessEntityType = await _businessEntityTypeRepository.Get().Where(x => x.BusinessEntityTypeName == BuisnessEntityName.Region.ToString()).FirstOrDefaultAsync();

            if (businessEntityType != null)
            {
                var getRegion = await _businessEntityRepository.Get().Where(x => x.BusinessEntityID == request.Id && x.BusinessEntityTypeId == businessEntityType.BusinessEntityTypeId && !x.IsDeleted).FirstOrDefaultAsync();

                if (getRegion != null)
                {
                    region.RegionID = getRegion.BusinessEntityID;
                    region.RegionName = getRegion.BusinessEntityName;
                    region.RegionDescription = getRegion.Description;
                    region.RegionStatus = getRegion.IsActive;

                    region.Yards = await (from BEH in _businessEntityHierarchyRepository.Get().Where(x => x.parentBusinessEntityId == getRegion.BusinessEntityID && x.IsActive && !x.IsDeleted)
                                          join BE in _businessEntityRepository.Get() on BEH.BusinessEntityID equals BE.BusinessEntityID 
                                          join BET in _businessEntityTypeRepository.Get() on BE.BusinessEntityTypeId equals BET.BusinessEntityTypeId
                                          where BET.BusinessEntityTypeName == BuisnessEntityName.Yard.ToString()
                                          select new BusinessEntityVM
                                          {
                                              BusinessEntityId = BE.BusinessEntityID,
                                              BusinessEntityName = BE.BusinessEntityName
                                          }).ToListAsync(cancellationToken);
                }
            }
            else
                throw new FCCException(ErrorMessage.INTERNAL_SERVER_ERROR);

            _logger.LogInformation($"END GetRegionByIdQueryHandler");
            return region;
        }
    }
}