using MediatR;
using OrganizationManagementService.Service.ViewModel.Region;

namespace OrganizationManagementService.Service.Queries.Region
{
    public class GetAllRegionByParentIdQuery:IRequest<List<BusinessEntityVM>>
    {
        public int parentBusinessEntityId { get; set; }
        public GetAllRegionByParentIdQuery(int _parentBusinessEntityId)
        {
                parentBusinessEntityId = _parentBusinessEntityId;
        }
    }
}
