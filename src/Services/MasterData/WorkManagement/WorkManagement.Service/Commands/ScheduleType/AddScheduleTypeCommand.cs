using MediatR;
using WorkManagement.Service.ViewModel.ScheduleType;

namespace WorkManagement.Service.Commands.ScheduleType
{
	public class AddScheduleTypeCommand : IRequest<ScheduleTypeViewModel>
    {
        public ScheduleTypeViewModel scheduleTypeViewModel { get; set; }
        public AddScheduleTypeCommand(ScheduleTypeViewModel _scheduleTypeViewModel)
        {
            scheduleTypeViewModel = _scheduleTypeViewModel;
        }
    }
}
