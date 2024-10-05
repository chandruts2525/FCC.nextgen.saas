using WorkManagement.Domain.Model;
using WorkManagement.Domain.ViewModel.UnitOfMeasure; 
using MediatR; 

namespace WorkManagement.Service.Commands.UnitOfMeasure
{
    public class ActivateDeactivateUnitOfMeasureCommand : IRequest<UnitMeasure>
    {
        public UnitOfMeasureResponseVM unitOfMeasureResponseVM { get; set; }
        public ActivateDeactivateUnitOfMeasureCommand(UnitOfMeasureResponseVM _unitOfMeasureResponseVM)
        {
            unitOfMeasureResponseVM = _unitOfMeasureResponseVM;


        }
    }
}
