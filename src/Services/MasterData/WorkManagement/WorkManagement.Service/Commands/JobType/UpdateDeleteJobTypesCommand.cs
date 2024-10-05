using MediatR;
using WorkManagement.Domain.Model;

namespace WorkManagement.Service.Commands.JobType
{
    public class UpdateDeleteJobTypesCommand : IRequest<JobTypes>
    {
        public JobTypes JobTypes { get; set; }
        public UpdateDeleteJobTypesCommand(JobTypes jobTypes)
        {
            JobTypes = jobTypes;
        }
    }
}
