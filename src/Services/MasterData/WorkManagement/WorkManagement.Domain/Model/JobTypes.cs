using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.Model
{
	public class JobTypes : MasterBaseEntity
    {
        [Key]
        public int JobTypeId { get; set; }
        public string? JobTypeCode { get; set; }
        public string? JobTypeName { get; set; }
    }
}
