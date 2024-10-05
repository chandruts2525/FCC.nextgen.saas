using FCC.Core.Constants;
using FCC.Core;
using WorkManagement.Domain.Model;
using WorkManagement.Service.ViewModel;
using MediatR;
using WorkManagement.Service.Commands.JobType;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.JobType
{
	public class UpdateDeleteJobTypesCommandHandler : IRequestHandler<UpdateDeleteJobTypesCommand, JobTypes>
	{
		private readonly IRepository<JobTypes> _jobTypeRepository;
		private readonly ILogger<UpdateDeleteJobTypesCommandHandler> _logger;

		public UpdateDeleteJobTypesCommandHandler(IRepository<JobTypes> jobTypeRepository, ILogger<UpdateDeleteJobTypesCommandHandler> logger)
		{
			_jobTypeRepository = jobTypeRepository;
			_logger = logger;
		}

		public async Task<JobTypes> Handle(UpdateDeleteJobTypesCommand request, CancellationToken cancellationToken)
		{
			var response = new JobTypes();
			_logger.LogInformation($"Start UpdateDeleteJobTypesCommandHandler");

			if (request.JobTypes.JobTypeName != "" && request.JobTypes.JobTypeCode != "")
			{
				var JobType = new JobTypes
				{
					JobTypeId = request.JobTypes.JobTypeId,
					JobTypeCode = request.JobTypes.JobTypeCode,
					JobTypeName = request.JobTypes.JobTypeName,
					IsActive = request.JobTypes.IsActive,
					IsDeleted = request.JobTypes.IsDeleted,
					ModifiedBy = "Administrator",
					ModifiedDateUTC = DateTime.UtcNow
				};

				var getJobtype = await _jobTypeRepository.Get().Where(x => (x.JobTypeCode == JobType.JobTypeCode || x.JobTypeName == JobType.JobTypeName) && !x.IsDeleted && x.JobTypeId != JobType.JobTypeId).ToListAsync(cancellationToken);

				if (getJobtype.Count == 0)
				{
					var result = await _jobTypeRepository.UpdateAsync(JobType, cancellationToken);
					if (result.JobTypeId == 0)
						throw new FCCException(ErrorMessage.JOBTYPE_SAVE_ERROR);
					response.JobTypeId = result.JobTypeId;
				}
				else
					response.JobTypeId = Convert.ToInt32(ResponseEnum.NotExists);

				_logger.LogInformation($"END UpdateEmployeeTypeCommandHandler");
				return response;

			}
			else
			{
				return new JobTypes();
			}
		}


	}
}
