using IAC.Domain.ViewModel.Role;
using MediatR;

namespace IAC.Service.Queries.Role
{
    public class GetRoleByIdQuery : IRequest<List<GetRoleByIdResponseVM>>
    {
        public GetRoleByIdResponseVM getRoleByIdResponseVM { get; set; }
        public GetRoleByIdQuery(GetRoleByIdResponseVM _getRoleByIdResponseVM)
        {
			getRoleByIdResponseVM = _getRoleByIdResponseVM;
        }
    }
}