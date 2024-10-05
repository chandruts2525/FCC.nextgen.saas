using WorkManagement.Domain.ViewModel.QuoteFooter;

namespace WorkManagement.Service.ViewModel.QuoteFooters
{
    public class GetAllQuoteFooterVM
    {
        public GetAllQuoteFooterVM()
        {
            QuoteFooters = new List<GetAllQuoteFooter>();
        }
        public int Count { get; set; }
        public List<GetAllQuoteFooter>? QuoteFooters { get; set; }
    }
}
