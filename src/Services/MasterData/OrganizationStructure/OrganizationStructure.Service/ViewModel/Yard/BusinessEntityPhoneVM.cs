using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.Service.ViewModel.Yard
{
    public class BusinessEntityPhoneVM
    {
        public int PhoneNumberTypeId { get; set; }
        public string? CountryCode { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
