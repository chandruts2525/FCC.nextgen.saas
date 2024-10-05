using WorkManagement.Domain.Model;
using WorkManagement.Service.Commands.QuoteFooter;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.QuoteFooter;
public class ActivateDeactivateQuoteFooterCommandHandler : IRequestHandler<ActivateDeactivateQuoteFooterCommand, Terms>
{
    private readonly IRepository<Terms> _iRepository;

    public ActivateDeactivateQuoteFooterCommandHandler(IRepository<Terms> iRepository)
    {
        _iRepository = iRepository;
    }

    public async Task<Terms> Handle(ActivateDeactivateQuoteFooterCommand request, CancellationToken cancellationToken)
    {
        var getTerm = await _iRepository.Get(a=>a.TermID == request.quoteFootersRequestView.QuoteFootersID).FirstOrDefaultAsync(cancellationToken);
        Terms term = new();
        if (getTerm != null)
        {
            getTerm.IsActive = request.quoteFootersRequestView.Status;
            term = await _iRepository.UpdateAsync(getTerm,cancellationToken);
        } 
        return term;
    }
}
