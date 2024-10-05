using FCC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationManagementService.Domain.Model
{
    public class Group:MasterBaseEntity
    {
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? GroupDescription { get; set; }
        public int GroupTypeID { get; set; }
    }
}
