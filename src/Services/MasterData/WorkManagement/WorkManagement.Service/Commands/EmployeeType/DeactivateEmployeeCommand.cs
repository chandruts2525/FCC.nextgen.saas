using MediatR;
using WorkManagement.Domain.Model;

namespace WorkManagement.Service.Commands.EmployeeType
{
	public class DeactivateEmployeeCommand: IRequest<EmployeeTypes>
    {
        public EmployeeTypes employeeTypes { get; set; }
        public DeactivateEmployeeCommand(EmployeeTypes _employeeTypes)
        {
            employeeTypes = _employeeTypes;
        }
    }
}
