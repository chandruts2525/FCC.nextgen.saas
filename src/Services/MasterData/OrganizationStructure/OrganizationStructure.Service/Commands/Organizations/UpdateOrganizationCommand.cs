using MediatR;
using OrganizationManagementService.Service.ViewModel.Organization;
using OrganizationStructure.Domain.Model;

namespace OrganizationStructure.Service.Commands.Organizations
{
    public class UpdateOrganizationCommand : IRequest<BusinessEntity>
    {
        public OrganizationVM OrganizationVM { get; set; }
        public UpdateOrganizationCommand(OrganizationVM _OrganizationVM)
        {
            OrganizationVM = _OrganizationVM;
        }
    }
}