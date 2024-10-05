using FCC.Core.Constants;
using FCC.Core;
using MediatR;
using System.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;
using OrganizationStructure.Service.Commands.Organizations;
using OrganizationStructure.Domain.Model;

namespace OrganizationStructure.Service.Handlers.Organizations
{
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, BusinessEntity>
    {
        private readonly IRepository<BusinessEntityType> _BusinessEntityTypeRepository;
        private readonly IRepository<Organization> _OrganizationRepository;
        private readonly IRepository<BusinessEntity> _BusinessEntityRepository;
        private readonly IRepository<BusinessEntityHierarchy> _BusinessEntityHierarchyRepository;
        private readonly ILogger<CreateOrganizationCommandHandler> _logger;
        public CreateOrganizationCommandHandler(IRepository<BusinessEntityType> businessEntityTypeRepository, IRepository<Organization> organizationRepository, IRepository<BusinessEntity> businessEntityRepository, IRepository<BusinessEntityHierarchy> businessEntityHierarchyRepository, ILogger<CreateOrganizationCommandHandler> logger)
        {
            _BusinessEntityTypeRepository = businessEntityTypeRepository;
            _BusinessEntityRepository = businessEntityRepository;
            _OrganizationRepository = organizationRepository;
            _BusinessEntityHierarchyRepository = businessEntityHierarchyRepository;
            _logger = logger;
        }

        public async Task<BusinessEntity> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var response = new BusinessEntity();
            _logger.LogInformation("Started CreateOrganizationCommendHandler for Parameter: {request.OrganizationVM.BusinessEntityName}", request.OrganizationVM.BusinessEntityName);
            if (request.OrganizationVM != null)
            {
                var checkBusinessEntityType = await _BusinessEntityTypeRepository.Get().Where(x => x.BusinessEntityTypeName!.ToLower() == BuisnessEntityName.Organization.ToString().ToLower()).FirstOrDefaultAsync(cancellationToken) ?? throw new FCCException(ErrorMessage.NOT_EXISTS);
                var checkBusinessEntity = await _BusinessEntityRepository.Get().Where(x => x.BusinessEntityTypeId == checkBusinessEntityType.BusinessEntityTypeId && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);
                if (checkBusinessEntity != null)
                    throw new FCCException(ErrorMessage.ORGANIZATION_ALREADY_EXISTS);

                var insertBusinessEntity=new BusinessEntity()
                {
                   BusinessEntityTypeId=checkBusinessEntityType.BusinessEntityTypeId,
                   BusinessEntityName=request.OrganizationVM.BusinessEntityName,
                   IsActive=true,
                   IsDeleted=false,
                   IsLocked=false,
                   ModifiedDate=null,
                   ModifiedBy=null
                };

                var createdBusinessEntity = await _BusinessEntityRepository.CreateAsync(insertBusinessEntity, cancellationToken);

                if(createdBusinessEntity.BusinessEntityID>0)
                {
                    response.BusinessEntityID = createdBusinessEntity.BusinessEntityID;
                    var insertOrganization = new Organization
                    {
                        BusinessEntityID=createdBusinessEntity.BusinessEntityID,
                        IsActive = true,
                        IsDeleted = false,
                        IsLocked = false,
                        ModifiedDateUTC = null,
                        ModifiedBy = null
                    };

                    var createdOrganization = await _OrganizationRepository.CreateAsync(insertOrganization, cancellationToken);
                    if (createdOrganization.BusinessEntityID == 0)
                        throw new FCCException(ErrorMessage.ORGANIZATION_SAVE_ERROR);
                    var insertButEntHierarchy = new BusinessEntityHierarchy
                    {
                        BusinessEntityID = createdBusinessEntity.BusinessEntityID,
                        parentBusinessEntityId = 0,
                        IsActive = true,
                        IsDeleted = false,
                        IsLocked = false,
                        ModifiedDateUTC = null,
                        ModifiedBy = null
                        
                    };
                    var createdBusEntHierarchy = await _BusinessEntityHierarchyRepository.CreateAsync(insertButEntHierarchy, cancellationToken);
                    if (createdBusEntHierarchy.BusinessEntityID == 0)
                        throw new FCCException(ErrorMessage.BUSINESSENTITYHIERARCHY_SAVE_ERROR);
                    
                }
                else
                {
                    throw new FCCException(ErrorMessage.BUSINESSENTITY_SAVE_ERROR);
                }
                _logger.LogInformation($"END CreateOrganizationCommendHandler");
                return response;

            }
            else
            {
                return response;
            }
        }
    }
}
