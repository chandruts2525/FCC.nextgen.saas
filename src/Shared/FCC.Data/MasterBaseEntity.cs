using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCC.Data
{
	public class MasterBaseEntity
    {
        public MasterBaseEntity() 
        {
			CreatedBy = "Admin";
            CreatedDateUTC = DateTime.UtcNow;
            ModifiedBy = "Admin";
        }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedDateUTC { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDateUTC { get; set; }
    }
}
