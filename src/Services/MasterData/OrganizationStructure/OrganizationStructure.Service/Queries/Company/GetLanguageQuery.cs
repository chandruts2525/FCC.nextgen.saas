using OrganizationStructure.Service.ViewModel.Company;
using MediatR;

namespace OrganizationStructure.Service.Queries.Company
{
    public class GetLanguageQuery : IRequest<List<LanguageVM>>
    {
        public GetLanguageQuery()
        { }
    }
}