using System.ComponentModel.DataAnnotations;

namespace WorkManagement.Domain.ViewModel.QuoteFooter
{
    public class GetAllQuoteFooter
    {
        public GetAllQuoteFooter()
        {

        }
        [Key]
        public int QuoteFooterID { get; set; }
        public string? QuoteFooterName { get; set; }
        public int CompanyCount { get; set; }
        public string? CompanyName { get; set; }
        public int ModuleCount { get; set; }
        public string? ModuleName { get; set; }
        public bool Status { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
