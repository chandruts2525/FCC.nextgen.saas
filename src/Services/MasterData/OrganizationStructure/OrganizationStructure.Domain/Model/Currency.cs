using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
    public class Currency : BaseEntity
    {
        [Key]
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
    }
}
