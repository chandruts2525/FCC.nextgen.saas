using WorkManagement.Domain.ViewModel.UnitOfMeasure;
using MediatR;

namespace WorkManagement.Service.Commands.UnitOfMeasure
{
    public class CreateUnitOfMeasureCommand : IRequest<UnitOfMeasureResponseVM>
    {
        public UnitOfMeasureResponseVM UnitOfMeasureVM { get; set; }
        public CreateUnitOfMeasureCommand(UnitOfMeasureResponseVM unitOfMeasureVM)
        {
            UnitOfMeasureVM = unitOfMeasureVM;
        }
    }
}
