using FCC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
    public class Country : BaseEntity
    {
        [Key]
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}
