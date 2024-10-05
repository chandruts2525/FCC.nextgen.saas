using OrganizationStructure.Domain.ViewModel.Company;
using MediatR;

namespace OrganizationStructure.Service.Commands.Company
{
    public class CreateCompanyCommand : IRequest<CreateCompanyResponseVM<int>>
    {
        public CreateCompanyVM createCompanyVM { get; set; }
        public CreateCompanyCommand(CreateCompanyVM _createCompanyVM)
        {
			createCompanyVM = _createCompanyVM;
        }
    }
}
