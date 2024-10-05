using FCC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkManagement.Service.ViewModel.ScheduleType
{
    public class ScheduleTypeViewModel 
    {
        public int ScheduleTypeId { get; set; }
        public string? ScheduleTypeName { get; set; }
        public string? ScheduleTypeCode { get; set; }
        public int? UnitOrComponent { get; set; }
        public bool Schedulable { get; set; }
        public bool GBP { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsLocked { get; set; }
		public string? CreatedBy { get; set; }
	}
}
