using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.Service.ViewModel.Yard
{
    public class TermsVM
    {
        public int TermID { get; set; }
        public int? TermTypeID { get; set; }
        public string? TermText { get; set; }
        public string? TermName { get; set; }
    }
}
