using IAC.Service.ViewModel.User;
using MediatR;

namespace IAC.Service.Commands.User
{
    public class CreateUserCommand : IRequest<CreateSecurityUserViewModel>
    {
        public CreateSecurityUserViewModel UserViewModel { get; set; }

        public CreateUserCommand(CreateSecurityUserViewModel userViewModel)
        {
            UserViewModel = userViewModel;
        }
    }
}
