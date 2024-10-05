using WorkManagement.Domain.Model;
using WorkManagement.Service.Queries.JobType;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.JobType
{
    public class GetAllJobTypesQueryHandler : IRequestHandler<GetAllJobTypesQuery, List<JobTypes>>
    {
        private readonly IRepository<JobTypes> _jobTypeRepository;
        private readonly ILogger<GetAllJobTypesQueryHandler> _logger;


        public GetAllJobTypesQueryHandler(IRepository<JobTypes> jobTypeRepository, ILogger<GetAllJobTypesQueryHandler> logger)
        {
            _jobTypeRepository = jobTypeRepository;
            _logger = logger;
        }
        async Task<List<JobTypes>> IRequestHandler<GetAllJobTypesQuery, List<JobTypes>>.Handle(GetAllJobTypesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Start GetAllJobTypesQueryHandler");
            var result = await _jobTypeRepository.Get().Where(x => !x.IsDeleted).ToListAsync(cancellationToken);
            _logger.LogInformation($"END GetAllJobTypesQueryHandler");
            if (result != null)
                return result;
            else
                return new List<JobTypes>();
        }
    }
}
