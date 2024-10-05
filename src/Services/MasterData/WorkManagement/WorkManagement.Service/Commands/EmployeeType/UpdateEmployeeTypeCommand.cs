using MediatR;
using WorkManagement.Domain.Model;

namespace WorkManagement.Service.Commands.EmployeeType
{
	public class UpdateEmployeeTypeCommand : IRequest<EmployeeTypes>
    {
        public EmployeeTypes EmployeeTypes { get; set; }
        public UpdateEmployeeTypeCommand(EmployeeTypes _EmployeeTypes)
        {
            EmployeeTypes = _EmployeeTypes;
        }
    }
}
