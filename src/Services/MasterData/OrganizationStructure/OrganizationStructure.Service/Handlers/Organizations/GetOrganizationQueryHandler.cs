using FCC.Core;
using FCC.Core.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationManagementService.Service.ViewModel.Organization;
using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.Queries.Organizations;
using Softura.EntityFrameworkCore.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.Service.Handlers.Organizations
{
    public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, OrganizationVM>
    {
        public readonly IRepository<BusinessEntity> _BusinessEntityRepository;
        public readonly IRepository<BusinessEntityType> _BusinessEntityTypeRepository;
        private readonly ILogger<GetOrganizationQueryHandler> _logger;
        public GetOrganizationQueryHandler(IRepository<BusinessEntityType> businessEntityTypeRepository,IRepository<BusinessEntity> businessEntityRepository, ILogger<GetOrganizationQueryHandler> logger)
        {
            _BusinessEntityTypeRepository = businessEntityTypeRepository;
            _BusinessEntityRepository = businessEntityRepository;
            _logger = logger;
        }
        async Task<OrganizationVM> IRequestHandler<GetOrganizationQuery, OrganizationVM>.Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Start GetOrganizationQueryHandler");
            var checkBusinessEntityType = await _BusinessEntityTypeRepository.Get().Where(x => x.BusinessEntityTypeName!.ToLower() == BuisnessEntityName.Organization.ToString().ToLower()).FirstOrDefaultAsync(cancellationToken) ?? throw new FCCException(ErrorMessage.NOT_EXISTS);
            var result = await _BusinessEntityRepository.Get().Where(x => x.BusinessEntityTypeId == checkBusinessEntityType.BusinessEntityTypeId && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);
            _logger.LogInformation($"GetOrganizationQueryHandler End");
            if(result!=null)
            {
                var response = new OrganizationVM()
                {
                    BusinessEntityID=result.BusinessEntityID,
                    BusinessEntityTypeId=result.BusinessEntityTypeId,
                    BusinessEntityCode=result.BusinessEntityCode,
                    BusinessEntityName=result.BusinessEntityName,
                    GLDistributionCode=result.GLDistributionCode,
                    IsActive=result.IsActive,
                    IsDeleted=result.IsDeleted,
                    IsLocked=result.IsLocked,
                    ModifiedBy =result.ModifiedBy,
                    CreatedBy =result.CreatedBy,
                    ModifiedDate =result.ModifiedDate,
                    CreatedDate =result.CreatedDate,
                };
                return response;
            }

            return new OrganizationVM() { BusinessEntityID=0};
        }
    }
}
