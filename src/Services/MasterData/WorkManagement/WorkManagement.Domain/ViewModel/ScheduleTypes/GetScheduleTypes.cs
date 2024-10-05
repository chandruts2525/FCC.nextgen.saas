using Microsoft.EntityFrameworkCore;
 
namespace WorkManagement.Domain.ViewModel.ScheduleTypes;

public class GetScheduleTypes
{
    public int ScheduleTypeId { get; set; }
    public string? ScheduleTypeCode { get; set; }
    public string? ScheduleTypeName { get; set; }
    public string? UnitOrComponent { get; set; }
    public bool? Schedulable { get; set; }
    public bool? GBP { get; set; }
    public bool IsActive { get; set; } 
    public string? CreatedBy { get; set; } 
    public DateTime CreatedDateUTC { get; set; } 
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDateUTC { get; set; }
}
