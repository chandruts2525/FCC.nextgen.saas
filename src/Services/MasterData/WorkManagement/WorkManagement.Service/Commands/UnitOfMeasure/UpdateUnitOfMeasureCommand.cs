using WorkManagement.Domain.ViewModel.UnitOfMeasure;
using MediatR;

namespace WorkManagement.Service.Commands.UnitOfMeasure
{
    public class UpdateUnitOfMeasureCommand : IRequest<UnitOfMeasureResponseVM>
    {
        public UnitOfMeasureResponseVM UnitOfMeasureVM { get; set; }
        public UpdateUnitOfMeasureCommand(UnitOfMeasureResponseVM unitOfMeasureVM)
        {
            UnitOfMeasureVM = unitOfMeasureVM;
        }
    }
}
