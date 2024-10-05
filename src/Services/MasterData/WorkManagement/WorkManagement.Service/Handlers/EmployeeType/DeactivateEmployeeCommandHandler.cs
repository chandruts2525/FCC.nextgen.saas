using WorkManagement.Domain.Model;
using WorkManagement.Service.Commands.EmployeeType;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.EmployeeType
{
	public class DeactivateEmployeeCommandHandler : IRequestHandler<DeactivateEmployeeCommand, EmployeeTypes>
    {
        private readonly IRepository<EmployeeTypes> _employeeTypes;

        private readonly ILogger<DeactivateEmployeeCommandHandler> _logger;

        public DeactivateEmployeeCommandHandler(IRepository<EmployeeTypes> employeeTypes, ILogger<DeactivateEmployeeCommandHandler> logger)
        {
            _employeeTypes = employeeTypes;
            _logger = logger;
        }

        public async Task<EmployeeTypes> Handle(DeactivateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var getjobtype = await _employeeTypes.Get().Where(x => x.EmployeeTypeId == request.employeeTypes.EmployeeTypeId).FirstOrDefaultAsync(cancellationToken);
            if (getjobtype != null)
            {
                getjobtype.IsActive = request.employeeTypes.IsActive;
                _logger.LogInformation("Start DeactivateEmployeeTypeDeactivateCommandHandler, Parameters: JobTypeId: {request.employeeTypes.EmployeeTypeId}", request.employeeTypes.EmployeeTypeId);
                var updatejobtype = await _employeeTypes.UpdateAsync(getjobtype,cancellationToken);
                if (updatejobtype != null  && updatejobtype.IsActive)
                    return new EmployeeTypes() { EmployeeTypeName = "Successfully Activated EmployeeType" };
                else if (updatejobtype != null && !updatejobtype.IsActive )
                    return new EmployeeTypes() { EmployeeTypeName = "Successfully Deactivated EmployeeType" };
                else
                    return new EmployeeTypes() { EmployeeTypeName = "Activate EmployeeType Failed" };
            }
            else
            {
                return new EmployeeTypes() { };
            }
        }
    }
}
