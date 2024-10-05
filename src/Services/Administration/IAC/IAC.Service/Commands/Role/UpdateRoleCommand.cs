using IAC.Domain.Model;
using IAC.Service.ViewModel.Role;
using MediatR;

namespace IAC.Service.Commands.Role
{
    public class UpdateRoleCommand : IRequest<UpdateRoleVM>
    {
        public UpdateRoleVM updateRoleVM { get; set; }
        public UpdateRoleCommand(UpdateRoleVM _updateRoleVM)
        {
			updateRoleVM = _updateRoleVM;
        }
    }
}

