using MediatR;
using OrganizationManagementService.Service.ViewModel.Organization;
using OrganizationStructure.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationStructure.Service.Queries.Organizations
{
    public class GetOrganizationQuery : IRequest<OrganizationVM>
    {
        public OrganizationVM OrganizationVM { get; set; }
        public GetOrganizationQuery(OrganizationVM _OrganizationVM)
        {
            OrganizationVM = _OrganizationVM;
        }
    }
}
