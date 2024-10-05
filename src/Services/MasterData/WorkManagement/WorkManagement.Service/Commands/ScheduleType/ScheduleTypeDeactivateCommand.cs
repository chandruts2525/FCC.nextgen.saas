using WorkManagement.Service.ViewModel.ScheduleType;
using MediatR;

namespace WorkManagement.Service.Commands.ScheduleType
{
    public class ScheduleTypeDeactivateCommand:IRequest<ScheduleTypeViewModel>
    {
        public int scheduleTypeId {  get; set; }
        public bool status { get; set; }    
        public ScheduleTypeDeactivateCommand(int _scheduleTypeId, bool _status)
        {
           scheduleTypeId = _scheduleTypeId;
           status = _status;
        }
    }
}
