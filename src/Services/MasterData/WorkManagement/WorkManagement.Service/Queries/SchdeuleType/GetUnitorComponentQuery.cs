using MediatR;
using WorkManagement.Domain.Model;
using WorkManagement.Domain.ViewModel.ScheduleTypes;

namespace WorkManagement.Service.Queries.SchdeuleType
{
	public class GetUnitorComponentQuery : IRequest<List<ScheduleTypeComponents>>
    {
        public UnitorComponentVM unitorComponentViewModel { get; set; }
        public GetUnitorComponentQuery(UnitorComponentVM _unitorComponentViewModel)
        {
            unitorComponentViewModel = _unitorComponentViewModel;
        }
    }
}