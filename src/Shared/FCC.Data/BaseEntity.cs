using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCC.Data
{
	public class BaseEntity
	{
        public BaseEntity() 
        {
			CreatedBy = "Admin";
            CreatedDate = DateTime.UtcNow;
            ModifiedBy = "Admin";
		}
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
