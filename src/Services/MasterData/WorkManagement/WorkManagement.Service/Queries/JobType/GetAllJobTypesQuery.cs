using WorkManagement.Domain.Model;
using MediatR;

namespace WorkManagement.Service.Queries.JobType
{
	public class GetAllJobTypesQuery : IRequest<List<JobTypes>>
    {

    }
}
