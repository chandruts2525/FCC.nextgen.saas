using IAC.Domain.ViewModel.Role;
using MediatR;

namespace IAC.Service.Queries.Role;

public class GetBusinessTypesQuery : IRequest<List<GetBusinessEntityByTypeResponseVM>>
{
    public GetBusinessEntityByTypeResponseVM getBusinessEntityByTypeResponseVM { get; set; }
    public GetBusinessTypesQuery(GetBusinessEntityByTypeResponseVM _getBusinessEntityByTypeResponseVM)
    {
		getBusinessEntityByTypeResponseVM = _getBusinessEntityByTypeResponseVM;
    }
}
