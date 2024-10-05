using FCC.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizationStructure.Domain.Model
{
    public class Language : BaseEntity
    {
        [Key]
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

    }
}
