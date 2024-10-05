using FCC.Core.Constants;
using FCC.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.Commands.Organizations;
using Softura.EntityFrameworkCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationManagementService.Service.Commands.Region;
using OrganizationManagementService.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace OrganizationManagementService.Service.Handlers.Region
{
    public class CreateRegionCommandHandler : IRequestHandler<CreateRegionCommand, BusinessEntity>
    {
        private readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
        private readonly IRepository<BusinessEntity> _businessEntityRepository;
        private readonly IRepository<BusinessEntityHierarchy> _businessEntityHierarchyRepository;
        private readonly ILogger<CreateRegionCommandHandler> _logger;
        public CreateRegionCommandHandler(IRepository<BusinessEntityType> businessEntityTypeRepository, IRepository<BusinessEntity> businessEntityRepository, IRepository<BusinessEntityHierarchy> businessEntityHierarchyRepository,
          ILogger<CreateRegionCommandHandler> logger)
        {
            _businessEntityTypeRepository = businessEntityTypeRepository;
            _businessEntityRepository = businessEntityRepository;
            _businessEntityHierarchyRepository = businessEntityHierarchyRepository;
            _logger = logger;
        }

        public async Task<BusinessEntity> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            var response = new BusinessEntity();
            try
            {
                
                _logger.LogInformation($"Start CreateRegionCommandHandler");

                var BusinessEntityType = await _businessEntityTypeRepository.Get().Where(p => p.BusinessEntityTypeName!.ToLower() == "region").FirstOrDefaultAsync(cancellationToken);
                if (BusinessEntityType!.BusinessEntityTypeId == 0)
                    throw new FCCException(ErrorMessage.NOT_EXISTS);
                var BusinessEntity = new BusinessEntity()
                {
                    BusinessEntityTypeId = BusinessEntityType!.BusinessEntityTypeId,
                    BusinessEntityName = request.RegionRequestVM.RegionName,
                    Description=request.RegionRequestVM.RegionDescription,
                    IsActive = true,
                    ModifiedBy = null,
                    ModifiedDate = null
                };
                var createdRegion = await _businessEntityRepository.CreateAsync(BusinessEntity,cancellationToken);
                if (createdRegion!.BusinessEntityID == 0)
                    throw new FCCException(ErrorMessage.REGION_SAVE_ERROR);
                response.BusinessEntityID = createdRegion.BusinessEntityID;
                var BusinessEntityHierarchy = new BusinessEntityHierarchy()
                {
                    BusinessEntityID = createdRegion.BusinessEntityID,
                    parentBusinessEntityId = Convert.ToInt32(request.RegionRequestVM.ParentBusinessEntityId),
                    IsActive = true,
                    ModifiedBy = null,
                    ModifiedDateUTC = null
                };
                var createdRegionBusinessEntityHierarchy = await _businessEntityHierarchyRepository.CreateAsync(BusinessEntityHierarchy, cancellationToken);
                if (createdRegionBusinessEntityHierarchy!.BusinessEntityID == 0)
                    throw new FCCException(ErrorMessage.REGION_SAVE_ERROR);
                if (!String.IsNullOrEmpty(request.RegionRequestVM.RegionYardsList))
                {
                    var mappedYards = request.RegionRequestVM.RegionYardsList?.Split(',');
                    if (mappedYards != null)
                    {
                        foreach (var mappedYard in mappedYards)
                        {
                            if (!string.IsNullOrEmpty(mappedYard))
                            {
                                var insertBusinessEntityHierarchy = new BusinessEntityHierarchy()
                                {
                                    BusinessEntityID = Convert.ToInt32(mappedYard),
                                    parentBusinessEntityId = createdRegion.BusinessEntityID,
                                    IsActive = true,
                                    ModifiedBy = null,
                                    ModifiedDateUTC = null
                                };
                                var createdYardBusinessEntityHierarchy = await _businessEntityHierarchyRepository.CreateAsync(insertBusinessEntityHierarchy, cancellationToken);

                                if (createdYardBusinessEntityHierarchy.BusinessEntityID == 0)
                                    throw new FCCException(ErrorMessage.YARD_SAVE_ERROR);
                            }
                        }
                    }
                }

                _logger.LogInformation($"END CreateRegionCommandHandler");
                return response;
            }
            catch (Exception)
            {
                _logger.LogError("Error in RegionController.CreateRegionCommandHandler");
                throw;
            }
        }

    }
}
