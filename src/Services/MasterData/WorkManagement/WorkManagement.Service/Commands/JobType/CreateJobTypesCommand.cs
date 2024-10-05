using MediatR;
using WorkManagement.Domain.Model;

namespace WorkManagement.Service.Commands.JobType
{
	public class CreateJobTypesCommand : IRequest<JobTypes>
    {
        public JobTypes JobTypes { get; set; }
        public CreateJobTypesCommand(JobTypes jobTypes)
        {
            JobTypes = jobTypes;
        }
    }

}
