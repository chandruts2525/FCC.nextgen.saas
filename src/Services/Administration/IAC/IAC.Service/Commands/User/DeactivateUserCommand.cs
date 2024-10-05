using IAC.Domain.Model;
using MediatR;

namespace IAC.Service.Commands.User
{
    public class DeactivateUserCommand : IRequest<SecurityUser>
    {
        public SecurityUser UserViewModel { get; set; }

        public DeactivateUserCommand(SecurityUser userViewModel)
        {
            UserViewModel = userViewModel;
        }


    }

}
