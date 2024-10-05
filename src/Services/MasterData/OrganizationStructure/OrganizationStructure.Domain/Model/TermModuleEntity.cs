using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class TermModuleEntity : MasterBaseEntity
    {
        [Key]
        public int TermModuleEntityID { get; set; }
        public int TermID { get; set; }
        public int ModuleID { get; set; }
    }
}
