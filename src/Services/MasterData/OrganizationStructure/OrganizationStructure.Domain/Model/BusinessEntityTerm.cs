using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
	public class BusinessEntityTerm : MasterBaseEntity
    {
        [Key]
        public int BusinessEntityTermID { get; set; }
        public int TermID { get; set; }
        public int BusinessEntityID { get; set; }
    }
}
