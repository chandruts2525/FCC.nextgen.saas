using MediatR;
using OrganizationManagementService.Service.ViewModel.Region;

namespace OrganizationManagementService.Service.Queries.Region
{
    public class GetRegionByIdQuery:IRequest<GetRegionByIdVM>
    {
        public int Id { get; set; }
        public GetRegionByIdQuery(int id) {
            Id = id;
        }
    }
}
