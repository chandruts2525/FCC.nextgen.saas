using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.Service.ViewModel.Yard
{
    public class GeneralLedgerVM
    {
        public int GeneralLedgerId { get; set; }
        public string? GeneralLedgerAccountCode { get; set; }
        public string? Description { get; set; }
    }
}
