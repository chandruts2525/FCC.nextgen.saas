using OrganizationStructure.Service.ViewModel.Company;
using MediatR;

namespace OrganizationStructure.Service.Queries.Company
{
    public class GetMeasurementTypeQuery : IRequest<List<MeasurementTypeVM>>
    {
        public GetMeasurementTypeQuery()
        { }
    }
}
