using WorkManagement.Service.ViewModel.QuoteFooters;
using MediatR;


namespace WorkManagement.Service.Queries.QuoteFooter
{
    public class GetCompanyQuery : IRequest<List<BusinessEntityVM>>
    {
        public GetCompanyQuery()
        { }
    }
}
