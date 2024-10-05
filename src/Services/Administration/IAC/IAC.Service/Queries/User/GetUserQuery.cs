using IAC.Domain.ViewModel.SecurityUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IAC.Service.Queries.User
{
    [Keyless]
    public class GetUserQuery : IRequest<GetUserResponseVM>
    {
        public GetUserResponseVM userViewModel { get; set; }
        public GetUserQuery(GetUserResponseVM _getUser)
        {
            userViewModel = _getUser;
        }
    }
}