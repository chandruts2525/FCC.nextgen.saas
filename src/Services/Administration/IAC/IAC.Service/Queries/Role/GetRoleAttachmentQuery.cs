using IAC.Domain.ViewModel.Role;
using MediatR;

namespace IAC.Service.Queries.Role
{
    public class GetRoleAttachmentQuery : IRequest<List<GetRoleAttachmentResponseVM>>
    {
        public int RoleId { get; set; }
        public GetRoleAttachmentQuery(int _roleid)
        {
            RoleId = _roleid;
        }
    }
}
