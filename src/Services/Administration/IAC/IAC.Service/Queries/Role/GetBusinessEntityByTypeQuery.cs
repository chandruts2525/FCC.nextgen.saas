using IAC.Domain.ViewModel.Role;
using MediatR;

namespace IAC.Service.Queries.Role
{
    public class GetBusinessEntityByTypeQuery : IRequest<List<GetBusinessEntityByTypeResponseVM>>
    {
        public GetBusinessEntityByTypeResponseVM getBusinessEntityByTypeResponseVM { get; set; }
        public GetBusinessEntityByTypeQuery(GetBusinessEntityByTypeResponseVM _getBusinessEntityByTypeResponseVM)
        {
            getBusinessEntityByTypeResponseVM = _getBusinessEntityByTypeResponseVM;
        }
    }

}
