using MediatR;
using OrganizationStructure.Service.ViewModel.Yard;

namespace OrganizationStructure.Service.Commands.Yard
{
    public class UpdateYardCommand : IRequest<UpdateYardVM>
    {
        public UpdateYardVM UpdateYard { get; set; }
        public UpdateYardCommand(UpdateYardVM _updateYardVM)
        {
            UpdateYard = _updateYardVM;
        }
    }
}

