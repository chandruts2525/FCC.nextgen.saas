using FCC.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationStructure.Domain.Model
{
    [Keyless]
    public class BusinessEntityPhone : MasterBaseEntity
    {
        public int PhoneNumberTypeId { get; set; }
        public int BusinessEntityId { get; set; }
        public string? CountryCode { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
