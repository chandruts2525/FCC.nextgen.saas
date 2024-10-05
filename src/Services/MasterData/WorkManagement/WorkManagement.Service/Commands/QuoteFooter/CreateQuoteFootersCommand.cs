using MediatR;
using WorkManagement.Service.ViewModel.QuoteFooter;

namespace WorkManagement.Service.Commands.QuoteFooter
{
	public class CreateQuoteFootersCommand : IRequest<QuoteFootersRequestVM>
    {
        public QuoteFootersRequestVM quoteFootersRequestVM { get; set; }
        public CreateQuoteFootersCommand(QuoteFootersRequestVM _quoteFootersRequestVM)
        {
            quoteFootersRequestVM = _quoteFootersRequestVM;
        }
    }
}
