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
    public class GetBusinessEntityQueryHandler : IRequestHandler<GetBusinessEntityQuery, List<BusinessEntityResponseModel>>
    {
        public readonly IRepository<BusinessEntity> _businessEntityRepository;
        public readonly IRepository<BusinessEntityType> _businessEntityTypeRepository;
        private readonly ILogger<GetBusinessEntityQueryHandler> _logger;
        public GetBusinessEntityQueryHandler(IRepository<BusinessEntity> businessEntityRepository,
            IRepository<BusinessEntityType> businessEntityTypeRepository, ILogger<GetBusinessEntityQueryHandler> logger)
        {
            _businessEntityRepository = businessEntityRepository;
            _businessEntityTypeRepository = businessEntityTypeRepository;
            _logger = logger;
        }
        async Task<List<BusinessEntityResponseModel>> IRequestHandler<GetBusinessEntityQuery, List<BusinessEntityResponseModel>>.Handle(GetBusinessEntityQuery request, CancellationToken cancellationToken)
        {
            
            _logger.LogInformation("Start GetBusinessEntityQueryHandler, Parameters: BusinessEntityName: {request.BusinessViewModel.BusinessEntityName}", request.BusinessViewModel.BusinessEntityName);
            var result = await (from BE in _businessEntityRepository.Get()
                                join BET in _businessEntityTypeRepository.Get() on BE.BusinessEntityTypeId equals BET.BusinessEntityTypeId
                                where (BET.BusinessEntityTypeName!=null && BET.BusinessEntityTypeName.ToLower() == BuisnessEntityName.Employee.ToString().ToLower())
                                select new BusinessEntityResponseModel
                                {
                                    BusinessEntityID = BE.BusinessEntityID,
                                    BusinessEntityName = BE.BusinessEntityName,
                                }
                                ).ToListAsync(cancellationToken);
            _logger.LogInformation($"GetBusinessEntityQueryHandler End");

            if (result != null)
                return result;
            else
                return new List<BusinessEntityResponseModel>();
        }
    }
}