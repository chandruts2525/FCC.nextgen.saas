using MediatR;
using OrganizationStructure.Service.ViewModel.Yard;

namespace OrganizationManagementService.Service.Commands.Yard
{
    public class CreateYardCommand : IRequest<CreateYardVM>
    {
        public CreateYardVM CreateYard { get; set; }
        public CreateYardCommand(CreateYardVM _createYard)
        {
            CreateYard = _createYard;
        }
    }
}
