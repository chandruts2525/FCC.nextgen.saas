using FCC.Core.Constants;
using FCC.Core;
using WorkManagement.Domain.Model;
using MediatR;
using WorkManagement.Service.Commands.JobType;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.JobType
{
    public class CreateJobTypesCommandHandler : IRequestHandler<CreateJobTypesCommand, JobTypes>
    {
        private readonly IRepository<JobTypes> _jobTypeRepository;
        private readonly ILogger<CreateJobTypesCommandHandler> _logger;

        public CreateJobTypesCommandHandler(IRepository<JobTypes> jobTypeRepository, ILogger<CreateJobTypesCommandHandler> logger)
        {
            _jobTypeRepository = jobTypeRepository;
            _logger = logger;
        }

        public async Task<JobTypes> Handle(CreateJobTypesCommand request, CancellationToken cancellationToken)
        {
            var response = new JobTypes();
            _logger.LogInformation($"Start CreateJobTypesCommandHandler");

            if (request.JobTypes.JobTypeName != "" && request.JobTypes.JobTypeCode != "")
            {
                var JobType = new JobTypes
                {
                    JobTypeCode = request.JobTypes.JobTypeCode,
                    JobTypeName = request.JobTypes.JobTypeName,
                    IsActive = request.JobTypes.IsActive,
                    ModifiedBy = null,
                    ModifiedDateUTC = null
                };

                //Validate if the JobTypename and JobTypeCode already exists in database
                var getJobtype = await _jobTypeRepository.Get().Where(x => (x.JobTypeCode == JobType.JobTypeCode || x.JobTypeName == JobType.JobTypeName) && !x.IsDeleted).ToListAsync(cancellationToken);

                if (getJobtype.Count == 0)
                {
                    var result = await _jobTypeRepository.CreateAsync(JobType,cancellationToken);
                    if (result.JobTypeId == 0)
                        throw new FCCException(ErrorMessage.JOBTYPE_SAVE_ERROR);
                    response.JobTypeId = result.JobTypeId;
                }
                else
                    response.JobTypeId = Convert.ToInt32(ResponseEnum.NotExists);

                _logger.LogInformation($"END CreateJobTypesCommandHandler");
                return response;
            }
            else
            {
                return new JobTypes();
            }
        }
    }
}
