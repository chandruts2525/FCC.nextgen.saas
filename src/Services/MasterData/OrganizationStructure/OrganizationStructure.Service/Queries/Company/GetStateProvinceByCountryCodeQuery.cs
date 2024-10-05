using OrganizationStructure.Service.ViewModel.Company;
using MediatR;

namespace OrganizationStructure.Service.Queries.Company
{
    public class GetStateProvinceByCountryCodeQuery : IRequest<List<StateProvinceByCountryCodeVM>>
    {
        public string CountryCode { get; set; }
        public GetStateProvinceByCountryCodeQuery(string _countryCode)
        {
            CountryCode = _countryCode;
        }
    }
}
