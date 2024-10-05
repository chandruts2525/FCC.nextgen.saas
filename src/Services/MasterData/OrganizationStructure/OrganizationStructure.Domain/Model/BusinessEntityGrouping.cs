using FCC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationManagementService.Domain.Model
{
    public class BusinessEntityGrouping:MasterBaseEntity
    {
        [Key]
        public int GroupId { get; set; }
        public int BusinessEntityId { get; set; }
        
    }
}
