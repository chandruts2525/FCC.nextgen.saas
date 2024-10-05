using MediatR;
using WorkManagement.Domain.Model;

namespace WorkManagement.Service.Commands.EmployeeType
{
	public class AddEmployeeCommand : IRequest<EmployeeTypes>
    {
        public EmployeeTypes EmployeeTypes { get; set; }
        public AddEmployeeCommand(EmployeeTypes _employeeTypes)
        {
            EmployeeTypes = _employeeTypes;
        }
    }
}
