using IAC.Domain.ViewModel.Role;
using IAC.Service.ViewModel.Role;
using MediatR;

namespace IAC.Service.Queries.Role
{
	public class GetAllUserByBusinessEntityQuery : IRequest<List<GetAllUserListFilterResponseVM>>
    {
        public GetAllUserByBusinessEntityVM getAllUserByBusinessEntityVM { get; set; }
        public GetAllUserByBusinessEntityQuery(GetAllUserByBusinessEntityVM _getAllUserByBusinessEntityVM)
        {
			getAllUserByBusinessEntityVM = _getAllUserByBusinessEntityVM;
        }
    }
}