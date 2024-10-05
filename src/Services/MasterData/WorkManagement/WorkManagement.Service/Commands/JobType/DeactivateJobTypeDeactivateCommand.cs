using MediatR;
using WorkManagement.Domain.Model;

namespace WorkManagement.Service.Commands.JobType
{
	public class DeactivateJobTypeDeactivateCommand : IRequest<JobTypes>
    {
        public JobTypes _jobTypes { get; set; }

        public DeactivateJobTypeDeactivateCommand(JobTypes jobTypes)
        {
            _jobTypes = jobTypes;
        }


    }
}
