using FCC.Core.Constants;
using IAC.Domain.Model;
using IAC.Domain.ViewModel.SecurityUser;
using IAC.Service.Queries.User;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrganizationStructure.Domain.Model;
using Softura.EntityFrameworkCore.Abstractions;

namespace IAC.Service.Handlers.User
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponseVM>
    {
        private readonly IRepository<SecurityRole> _securityRoleRepository;
        private readonly IRepository<SecurityUser> _securityUserRepository;
        public readonly IRepository<SecurityUserBusinessEntity> _securityUserBusinessEntityRepository;
        public readonly IRepository<BusinessEntity> _businessEntityRepository;
        public readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
        private readonly ILogger<GetUserQueryHandler> _logger;
        public GetUserQueryHandler(IRepository<SecurityRole> securityRoleRepository,
            IRepository<SecurityUser> securityUserRepository,
            IRepository<SecurityUserBusinessEntity> securityUserBusinessEntityRepository,
            IRepository<BusinessEntity> businessEntityRepository,
            IRepository<BusinessEntityType> businessEntityTypeRepository,
            ILogger<GetUserQueryHandler> logger)
        {
            _securityRoleRepository = securityRoleRepository;
            _securityUserRepository = securityUserRepository;
            _securityUserBusinessEntityRepository = securityUserBusinessEntityRepository;
            _businessEntityRepository = businessEntityRepository;
            _businessEntityTypeRepository = businessEntityTypeRepository;
            _logger = logger;
        }
        async Task<GetUserResponseVM> IRequestHandler<GetUserQuery, GetUserResponseVM>.Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            
            _logger.LogInformation("Start GetUserQueryHandler, Parameters: UserId: {request.userViewModel.UserId}", request.userViewModel.UserId);

            var result = await (from u in _securityUserRepository.Get(x => x.UserId == request.userViewModel.UserId)
                                join r in _securityRoleRepository.Get() on u.RoleID equals r.RoleId
                                where  !u.IsDeleted
                                select new GetUserResponseVM
                                {
                                    UserId = u.UserId,
                                    LoginEmail = u.LoginEmail,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    MaximumATOMDevices = u.MaximumATOMDevices,
                                    RoleID = r.RoleId,
                                    RoleName = r.RoleName,
                                    Status = u.IsActive,

                                }).FirstOrDefaultAsync(cancellationToken);

            if (result != null)
            {
                result.Employee = await GetBusinessEntities(request, BuisnessEntityName.Employee.ToString(), cancellationToken);
                result.Companies = await GetBusinessEntities(request, BuisnessEntityName.Company.ToString(), cancellationToken);
                result.Yards = await GetBusinessEntities(request, BuisnessEntityName.Yard.ToString(), cancellationToken);
                result.Departments = await GetBusinessEntities(request, BuisnessEntityName.Department.ToString(), cancellationToken);
            }
            _logger.LogInformation($"GetUserQueryHandler End");

            if (result != null)
                return result;
            else
                return new GetUserResponseVM();
        }

        private async Task<List<BusinessEntityVM>> GetBusinessEntities(GetUserQuery request, string BusinessEntityTypeName,CancellationToken cancellationToken)
        {
            return await (from u in _securityUserBusinessEntityRepository.Get(x => x.UserId == request.userViewModel.UserId)
                          join BE in _businessEntityRepository.Get() on u.BusinessEntityId equals BE.BusinessEntityID
                          join BETE in _businessEntityTypeRepository.Get() on BE.BusinessEntityTypeId equals BETE.BusinessEntityTypeId
                          where !u.IsDeleted
                          && (BETE.BusinessEntityTypeName!=null && BETE.BusinessEntityTypeName.ToLower() == BusinessEntityTypeName.ToLower())
                          select new BusinessEntityVM
                          {
                              BusinessEntityId = BE.BusinessEntityID,
                              BusinessEntityName = BE.BusinessEntityName,
                              BusinessEntityTypeName = BETE.BusinessEntityTypeName
                          }
                                   ).ToListAsync(cancellationToken);
        }
    }
}
