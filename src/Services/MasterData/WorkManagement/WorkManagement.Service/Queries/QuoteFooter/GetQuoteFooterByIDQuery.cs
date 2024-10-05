using WorkManagement.Service.ViewModel.QuoteFooter;
using MediatR;

namespace WorkManagement.Service.Queries.QuoteFooter
{
    public class GetQuoteFooterByIDQuery : IRequest<QuoteFootersByIdvm>
    {
        public int Id { get; set; }
        public GetQuoteFooterByIDQuery(int _id)
        {
            Id = _id;
        }
    }
}
