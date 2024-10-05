using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.Model
{
	public class Terms : MasterBaseEntity
    {
        [Key]
        public int TermID { get; set; }
        public int? TermTypeID { get; set; }
        public string? TermText { get; set; }
        public string? TermName { get; set; }
    }
}
