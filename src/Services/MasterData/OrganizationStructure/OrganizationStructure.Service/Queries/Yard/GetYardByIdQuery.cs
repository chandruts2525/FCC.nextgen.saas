using MediatR;
using OrganizationStructure.Service.ViewModel.Yard;

namespace OrganizationManagementService.Service.Queries.Yard
{
    public class GetYardByIdQuery : IRequest<GetYardByIdResponseVM>
    {
        public GetYardByIdResponseVM GetYardById { get; set; }
        public GetYardByIdQuery(GetYardByIdResponseVM _getYardByIdResponseVM)
        {
            GetYardById = _getYardByIdResponseVM;
        }
    }
}