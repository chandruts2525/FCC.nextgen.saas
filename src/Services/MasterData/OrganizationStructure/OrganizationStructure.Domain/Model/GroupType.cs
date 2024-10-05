using FCC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationManagementService.Domain.Model
{
    public class GroupType:MasterBaseEntity
    {
        public int GroupTypeId { get; set; }
        public string? GroupTypeName { get; set; }
    }
}
