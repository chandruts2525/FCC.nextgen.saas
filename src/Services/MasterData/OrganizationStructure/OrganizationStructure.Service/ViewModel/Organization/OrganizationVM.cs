using FCC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationManagementService.Service.ViewModel.Organization
{
    public class OrganizationVM:BaseEntity
    {
        public int BusinessEntityID { get; set; }
        public int BusinessEntityTypeId { get; set; }
        public string? BusinessEntityName { get; set; }
        public string? BusinessEntityCode { get; set; }
        public string? GLDistributionCode { get; set; }
    }
}
