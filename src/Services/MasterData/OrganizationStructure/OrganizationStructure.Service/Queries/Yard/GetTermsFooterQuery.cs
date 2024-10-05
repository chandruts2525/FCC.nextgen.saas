using MediatR;
using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.ViewModel.Yard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationManagementService.Service.Queries.Yard
{
    public class GetTermsFooterQuery : IRequest<List<TermsVM>>
    {
        public GetTermsFooterQuery()
        { }
    }
}
