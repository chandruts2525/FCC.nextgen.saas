using OrganizationStructure.Service.ViewModel.Company;
using MediatR;

namespace OrganizationStructure.Service.Queries.Company
{
    public class GetCurrencyQuery : IRequest<List<CurrencyVM>>
    {
        public GetCurrencyQuery()
        { }
    }
}
