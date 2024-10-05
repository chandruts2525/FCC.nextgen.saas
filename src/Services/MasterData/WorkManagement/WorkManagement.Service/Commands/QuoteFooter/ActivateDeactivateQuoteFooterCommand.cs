using MediatR;
using WorkManagement.Domain.Model;
using WorkManagement.Service.ViewModel.QuoteFooter;

namespace WorkManagement.Service.Commands.QuoteFooter;

public class ActivateDeactivateQuoteFooterCommand : IRequest<Terms>
{
    public QuoteFootersRequestVM quoteFootersRequestView { get; set; }
    public ActivateDeactivateQuoteFooterCommand(QuoteFootersRequestVM quoteFootersRequestViewModel)
    {
        quoteFootersRequestView = quoteFootersRequestViewModel;


    }
}
