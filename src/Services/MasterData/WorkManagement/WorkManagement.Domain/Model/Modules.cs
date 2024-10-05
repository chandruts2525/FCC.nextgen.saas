using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.Model
{
	public class Modules : MasterBaseEntity
    {
        [Key]
        public int ModuleID { get; set; }
        public string? ModuleName { get; set; }
    }
}
