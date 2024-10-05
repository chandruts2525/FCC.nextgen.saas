using MediatR;
using WorkManagement.Service.ViewModel.QuoteFooter;

namespace WorkManagement.Service.Commands.QuoteFooter
{
	public class UpdateQuoteFootersCommand : IRequest<QuoteFootersRequestVM>
    {
        public QuoteFootersRequestVM _QuoteFootersRequest { get; set; }
        public UpdateQuoteFootersCommand(QuoteFootersRequestVM QuoteFootersRequest)
        {
            _QuoteFootersRequest = QuoteFootersRequest;
        }
    }
}
