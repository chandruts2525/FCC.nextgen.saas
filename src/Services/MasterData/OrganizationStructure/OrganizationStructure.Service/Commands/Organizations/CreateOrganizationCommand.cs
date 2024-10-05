using MediatR;
using OrganizationManagementService.Service.ViewModel.Organization;
using OrganizationStructure.Domain.Model;

namespace OrganizationStructure.Service.Commands.Organizations
{
    public class CreateOrganizationCommand : IRequest<BusinessEntity>
    {
        public OrganizationVM OrganizationVM { get; set; }
        public CreateOrganizationCommand(OrganizationVM _OrganizationVM)
        {
            OrganizationVM = _OrganizationVM;
        }
    }
}