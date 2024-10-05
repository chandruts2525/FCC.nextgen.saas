using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
    public class Organization:MasterBaseEntity
    {
        
        public Guid? OrganizationID { get; set; }
        public int BusinessEntityID { get; set; }
        

    }
}
