using WorkManagement.Service.ViewModel.ScheduleType;
using MediatR;

namespace WorkManagement.Service.Commands.ScheduleType
{
    public class UpdateScheduleTypeCommand : IRequest<ScheduleTypeViewModel>
    {
        public ScheduleTypeViewModel scheduleTypeViewModel { get; set; }
        public UpdateScheduleTypeCommand(ScheduleTypeViewModel _scheduleTypeViewModel)
        {
            scheduleTypeViewModel = _scheduleTypeViewModel;
        }
    }
}
