using IAC.Domain.Model;
using MediatR;

namespace IAC.Service.Commands.User
{
    public class CreateSecurityUserCommand : IRequest<SecurityUser>
    {

        public SecurityUser UserViewModel { get; set; }

        public CreateSecurityUserCommand(SecurityUser userViewModel)
        {
            UserViewModel = userViewModel;
        }


    }
}
