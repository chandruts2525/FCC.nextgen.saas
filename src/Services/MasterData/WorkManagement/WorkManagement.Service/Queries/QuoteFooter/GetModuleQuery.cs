using WorkManagement.Service.ViewModel.QuoteFooters;
using MediatR;

namespace WorkManagement.Service.Queries.QuoteFooter
{
    public class GetModuleQuery : IRequest<List<ModulesVM>>
    {
        public GetModuleQuery()
        {
        }
    }
}
