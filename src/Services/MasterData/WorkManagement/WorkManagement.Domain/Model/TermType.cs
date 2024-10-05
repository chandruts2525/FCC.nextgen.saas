using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.Model
{
	public class TermType : MasterBaseEntity
    {
        [Key]
        public int TermTypeID { get; set; }
        public string? TermTypeName { get; set; }

    }
}
