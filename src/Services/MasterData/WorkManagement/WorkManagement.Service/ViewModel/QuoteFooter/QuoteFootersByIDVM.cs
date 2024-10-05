using WorkManagement.Service.ViewModel.QuoteFooters;

namespace WorkManagement.Service.ViewModel.QuoteFooter
{
    public class QuoteFootersByIdvm
    {
        public QuoteFootersByIdvm()
        {
            Company = new List<BusinessEntityVM>();
            Modules = new List<ModulesVM>();

        }
        public int? QuoteFooterId { get; set; }
        public string? QuoteFooterName { get; set; }
        public bool Status { get; set; }
        public string? Description { get; set; }
        public List<BusinessEntityVM> Company { get; set; }
        public List<ModulesVM> Modules { get; set; }
    }
}
