using Microsoft.EntityFrameworkCore;
 
namespace WorkManagement.Domain.ViewModel.ScheduleTypes;

[Keyless]
public class ScheduleTypeResponseVM
{
    public int Count { get; set; }
    public List<GetScheduleTypes>? ScheduleTypes { get; set; }
}
