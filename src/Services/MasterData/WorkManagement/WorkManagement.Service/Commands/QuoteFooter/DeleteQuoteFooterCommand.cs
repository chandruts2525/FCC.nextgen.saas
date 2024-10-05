using MediatR;


namespace IAC.Service.Commands.QuoteFooter
{
    public class DeleteQuoteFooterCommand : IRequest<int?>
    {
        public int QuoteFooterId { get; set; }
        public DeleteQuoteFooterCommand(int _quoteFooterId)
        {
            QuoteFooterId = _quoteFooterId;
        }
    }
}
