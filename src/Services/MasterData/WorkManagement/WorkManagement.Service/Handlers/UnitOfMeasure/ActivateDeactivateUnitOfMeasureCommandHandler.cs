using WorkManagement.Domain.Model;
using WorkManagement.Service.Commands.UnitOfMeasure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.QuoteFooter;
public class ActivateDeactivateUnitOfMeasureCommandHandler : IRequestHandler<ActivateDeactivateUnitOfMeasureCommand, UnitMeasure>
{
    private readonly IRepository<UnitMeasure> _iRepository;

    public ActivateDeactivateUnitOfMeasureCommandHandler(IRepository<UnitMeasure> iRepository)
    {
        _iRepository = iRepository;
    }

    public async Task<UnitMeasure> Handle(ActivateDeactivateUnitOfMeasureCommand request, CancellationToken cancellationToken)
    {
        var getTerm = await _iRepository.Get(a=>a.UnitMeasureId == request.unitOfMeasureResponseVM.UnitMeasureId).FirstOrDefaultAsync(cancellationToken);
        UnitMeasure uom = new();
        if (getTerm != null)
        {
            getTerm.IsActive = request.unitOfMeasureResponseVM.IsActive;
            uom = await _iRepository.UpdateAsync(getTerm,cancellationToken);
        } 
        return uom;
    }
}
