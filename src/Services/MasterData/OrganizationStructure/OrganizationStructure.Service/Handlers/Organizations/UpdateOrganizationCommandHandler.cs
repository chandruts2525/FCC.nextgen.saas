using FCC.Core;
using FCC.Core.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.Commands.Organizations;
using Softura.EntityFrameworkCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.Service.Handlers.Organizations
{
    
        public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, BusinessEntity>
        {
            private readonly IRepository<BusinessEntityType> _BusinessEntityTypeRepository;
            private readonly IRepository<Organization> _OrganizationRepository;
            private readonly IRepository<BusinessEntity> _BusinessEntityRepository;
            private readonly IRepository<BusinessEntityHierarchy> _BusinessEntityHierarchyRepository;
            private readonly ILogger<UpdateOrganizationCommandHandler> _logger;

            public UpdateOrganizationCommandHandler(IRepository<BusinessEntityType> businessEntityTypeRepository, IRepository<Organization> organizationRepository, IRepository<BusinessEntity> businessEntityRepository, IRepository<BusinessEntityHierarchy> businessEntityHierarchyRepository, ILogger<UpdateOrganizationCommandHandler> logger)
            {
                _BusinessEntityTypeRepository= businessEntityTypeRepository;
                _BusinessEntityRepository = businessEntityRepository;
                _OrganizationRepository = organizationRepository;
                _BusinessEntityHierarchyRepository = businessEntityHierarchyRepository;
                _logger = logger;
            }

            public async Task<BusinessEntity> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
            {
                var response = new BusinessEntity();
                _logger.LogInformation("Start UpdateOrganisationCommandHandler for Parameter {request.OrganizationVM.BusinessEntityName}", request.OrganizationVM.BusinessEntityName);

                if (request.OrganizationVM.BusinessEntityID > 0)
                {
                    
                    var getBusinessEntity = await _BusinessEntityRepository.Get().Where(x => x.BusinessEntityID == request.OrganizationVM.BusinessEntityID && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);
                    if (getBusinessEntity != null)
                    {
                        var checkBusinessEntityType = await _BusinessEntityTypeRepository.Get().Where(x => x.BusinessEntityTypeName!.ToLower() == BuisnessEntityName.Organization.ToString().ToLower()).FirstOrDefaultAsync(cancellationToken) ?? throw new FCCException(ErrorMessage.NOT_EXISTS);
                        var BusEntUpdate = new BusinessEntity()
                        {
                            BusinessEntityID = request.OrganizationVM.BusinessEntityID,
                            BusinessEntityTypeId = checkBusinessEntityType.BusinessEntityTypeId,
                            BusinessEntityName = request.OrganizationVM.BusinessEntityName,
                            IsActive = request.OrganizationVM.IsActive,
                            IsDeleted = request.OrganizationVM.IsDeleted,
                            IsLocked = request.OrganizationVM.IsLocked,
                            ModifiedDate = DateTime.UtcNow

                        };
                        var updatedBusinessEntity = await _BusinessEntityRepository.UpdateAsync(BusEntUpdate, cancellationToken);
                        if (updatedBusinessEntity.BusinessEntityID == 0)
                            throw new FCCException(ErrorMessage.BUSINESSENTITY_UPDATE_ERROR);
                        response.BusinessEntityID = updatedBusinessEntity.BusinessEntityID;
                        var getOrganization = await _OrganizationRepository.Get().Where(x => x.BusinessEntityID == request.OrganizationVM.BusinessEntityID && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);
                        if(getOrganization != null)
                        {
                            var OrgUpdate = new Organization
                            {
                                BusinessEntityID = request.OrganizationVM.BusinessEntityID,
                                OrganizationID = getOrganization.OrganizationID,
                                IsActive = request.OrganizationVM.IsActive,
                                IsDeleted = request.OrganizationVM.IsDeleted,
                                IsLocked = request.OrganizationVM.IsLocked,
                                ModifiedDateUTC = DateTime.UtcNow
                            };
                            var updatedOrganization = await _OrganizationRepository.UpdateAsync(OrgUpdate, cancellationToken);
                            if (updatedOrganization.BusinessEntityID == 0)
                                throw new FCCException(ErrorMessage.ORGANIZATION_UPDATE_ERROR);
                        }
                        else
                        {
                            response.BusinessEntityID = Convert.ToInt32(ResponseEnum.NotExists);
                        }
                        var getBusEntHierarchy = await _BusinessEntityHierarchyRepository.Get().Where(x => x.BusinessEntityID == request.OrganizationVM.BusinessEntityID && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);
                        if (getBusEntHierarchy != null)
                        {
                            var BusEntHierarchyUpdate = new BusinessEntityHierarchy()
                            {
                                HierarchyId = getBusEntHierarchy.HierarchyId,
                                BusinessEntityID = request.OrganizationVM.BusinessEntityID,
                                parentBusinessEntityId = getBusEntHierarchy.parentBusinessEntityId,
                                IsActive = request.OrganizationVM.IsActive,
                                IsDeleted = request.OrganizationVM.IsDeleted,
                                IsLocked = request.OrganizationVM.IsLocked,
                                ModifiedDateUTC = DateTime.UtcNow
                            };
                            var updatedBusinessEntityHierarchy = await _BusinessEntityHierarchyRepository.UpdateAsync(BusEntHierarchyUpdate, cancellationToken);
                            if (updatedBusinessEntityHierarchy.BusinessEntityID == 0)
                                throw new FCCException(ErrorMessage.BUSINESSENTITYHIERARCHY_UPDATE_ERROR);
                        }
                        else
                        {
                            response.BusinessEntityID = Convert.ToInt32(ResponseEnum.NotExists);
                        }
                    }
                    else
                        response.BusinessEntityID = Convert.ToInt32(ResponseEnum.NotExists);

                    _logger.LogInformation($"END UpdateOrganizationCommandHandler");
                    return response;
                }
                else
                {
                    return response;
                }
            
            }
        }
    

}
