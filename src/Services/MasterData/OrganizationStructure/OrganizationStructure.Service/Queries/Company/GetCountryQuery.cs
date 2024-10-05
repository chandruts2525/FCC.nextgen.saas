using OrganizationStructure.Service.ViewModel.Company;
using MediatR;

namespace OrganizationStructure.Service.Queries.Company
{
    public class GetCountryQuery : IRequest<List<CountryVM>>
    {
        public GetCountryQuery()
        { }
    }
}
