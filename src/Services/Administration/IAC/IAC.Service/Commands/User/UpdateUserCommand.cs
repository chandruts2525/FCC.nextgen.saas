using IAC.Domain.Model;
using IAC.Service.ViewModel.User;
using MediatR;

namespace IAC.Service.Commands.User
{
    public class UpdateUserCommand : IRequest<UpdateSecurityUserViewModel>
    {

        public UpdateSecurityUserViewModel UserViewModel { get; set; }

        public UpdateUserCommand(UpdateSecurityUserViewModel userViewModel)
        {
            UserViewModel = userViewModel;
        }


    }
}
