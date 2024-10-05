using FCC.Core;
using FCC.Core.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationManagementService.Service.Commands.Region;
using OrganizationManagementService.Service.ViewModel.Region;
using OrganizationStructure.Domain.Model;
using Softura.EntityFrameworkCore.Abstractions;

namespace OrganizationManagementService.Service.Handlers.Region
{
    public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand, RegionRequestVM>
    {
        private readonly IRepository<BusinessEntity> _businessEntityRepository;
        private readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
        private readonly IRepository<BusinessEntityHierarchy> _businessEntityHierarchyRepository;
        private readonly ILogger<UpdateRegionCommandHandler> _logger;

        public UpdateRegionCommandHandler(IRepository<BusinessEntity> businessEntityRepository, IRepository<BusinessEntityType> businessEntityTypeRepository, IRepository<BusinessEntityHierarchy> businessEntityHierarchyRepository, ILogger<UpdateRegionCommandHandler> logger)
        {
            _businessEntityRepository = businessEntityRepository;
            _businessEntityTypeRepository = businessEntityTypeRepository;
            _businessEntityHierarchyRepository = businessEntityHierarchyRepository;
            _logger = logger;
        }

        public async Task<RegionRequestVM> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
        {
            RegionRequestVM response = new();
            _logger.LogInformation($"Start UpdateRegionCommandHandler");

            var businessEntityType = await _businessEntityTypeRepository.Get().Where(x => x.BusinessEntityTypeName == BuisnessEntityName.Region.ToString()).FirstOrDefaultAsync();

            if (businessEntityType != null)
            {
                var getregion = await _businessEntityRepository.Get().Where(x => x.BusinessEntityID == request.Region.RegionID && x.BusinessEntityTypeId == businessEntityType.BusinessEntityTypeId && !x.IsDeleted).FirstOrDefaultAsync();

                if (getregion != null)
                {
                    var count = _businessEntityRepository.Get().Where(x => x.BusinessEntityName == request.Region.RegionName && x.BusinessEntityTypeId == businessEntityType.BusinessEntityTypeId && x.BusinessEntityID != request.Region.RegionID && !x.IsDeleted).Count();

                    if (count == 0)
                    {
                        var updateRegion = new BusinessEntity()
                        {
                            BusinessEntityID = Convert.ToInt32(request.Region.RegionID),
                            BusinessEntityName = request.Region.RegionName,
                            BusinessEntityTypeId = businessEntityType.BusinessEntityTypeId,
                            Description = request.Region.RegionDescription,
                            IsActive = request.Region.RegionStatus,
                            CreatedBy = getregion.CreatedBy,
                            CreatedDate = getregion.CreatedDate
                        };

                        var update = await _businessEntityRepository.UpdateAsync(updateRegion);
                        if (update.BusinessEntityID == 0)
                            throw new FCCException(ErrorMessage.REGION_UPDATE_ERROR);

                        if (!string.IsNullOrEmpty(request.Region.RegionYardsList))
                        {
                            var yards = await (from BEH in _businessEntityHierarchyRepository.Get().Where(x => x.parentBusinessEntityId == request.Region.RegionID && x.IsActive && !x.IsDeleted)
                                               join BE in _businessEntityRepository.Get() on BEH.BusinessEntityID equals BE.BusinessEntityID
                                               join BET in _businessEntityTypeRepository.Get() on BE.BusinessEntityTypeId equals BET.BusinessEntityTypeId
                                               where BET.BusinessEntityTypeName == BuisnessEntityName.Yard.ToString()
                                               select new
                                               {
                                                   BEH.HierarchyId,
                                                   BusinessEntityId = BE.BusinessEntityID,
                                                   BEH.CreatedBy,
                                                   BEH.CreatedDateUTC
                                               }).ToListAsync(cancellationToken);

                            var selectedyardsList = request.Region.RegionYardsList.Split(',').ToList();

                            var updateYardIds = yards.Where(x => selectedyardsList != null && !selectedyardsList.Exists(y => x.BusinessEntityId.ToString().Equals(y))).ToList();
                            var insertYardsIds = selectedyardsList?.Where(x => !yards.Exists(y => x.Equals(y.BusinessEntityId.ToString()))).ToList();

                            if (updateYardIds.Count > 0)
                            {
                                var UpdateYards = updateYardIds.Select(x => new BusinessEntityHierarchy
                                {
                                    HierarchyId = x.HierarchyId,
                                    parentBusinessEntityId = update.BusinessEntityID,
                                    BusinessEntityID = x.BusinessEntityId,
                                    IsActive = false,
                                    IsDeleted = true,
                                    CreatedBy = x.CreatedBy,
                                    CreatedDateUTC = x.CreatedDateUTC                                    
                                }).ToList();
                                try
                                {
                                    await _businessEntityHierarchyRepository.UpdateAsync(UpdateYards, cancellationToken);
                                }
                                catch (Exception)
                                {
                                    throw new FCCException(ErrorMessage.BUSINESSENTITYHIERARCHY_UPDATE_ERROR);
                                }
                            }

                            if (insertYardsIds?.Count > 0)
                            {
                                var insertYardsList = insertYardsIds.Select(x => new BusinessEntityHierarchy
                                {
                                    BusinessEntityID = Convert.ToInt32(x),
                                    parentBusinessEntityId = update.BusinessEntityID,
                                    IsActive = true                                    
                                }).ToList();
                                try
                                {
                                    await _businessEntityHierarchyRepository.CreateAsync(insertYardsList, cancellationToken);
                                }
                                catch (Exception)
                                {
                                    throw new FCCException(ErrorMessage.BUSINESSENTITYHIERARCHY_UPDATE_ERROR);
                                }
                            }
                        }
                        response.RegionID = update.BusinessEntityID;
                    }
                    else
                        response.RegionID = -1;
                }                

                _logger.LogInformation($"END UpdateRegionCommandHandler");
                return response;
            }
            else
                throw new FCCException(ErrorMessage.INTERNAL_SERVER_ERROR);
            
        }
    }
}
