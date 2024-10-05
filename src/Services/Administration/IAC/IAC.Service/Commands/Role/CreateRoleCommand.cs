using IAC.Service.ViewModel.Role;
using MediatR;

namespace IAC.Service.Commands.Role
{
    public class CreateRoleCommand : IRequest<CreateRoleVM>
    {
        public CreateRoleVM createRoleVM { get; set; }
        public CreateRoleCommand(CreateRoleVM _createRoleVM)
        {
			createRoleVM = _createRoleVM;
        }
    }
}
