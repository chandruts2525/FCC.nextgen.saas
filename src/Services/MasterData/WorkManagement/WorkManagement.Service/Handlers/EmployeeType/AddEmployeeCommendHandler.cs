using FCC.Core.Constants;
using FCC.Core;
using WorkManagement.Domain.Model;
using MediatR;
using System.Data;
using Microsoft.Extensions.Logging;
using WorkManagement.Service.Commands.EmployeeType;
using Microsoft.EntityFrameworkCore;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.EmployeeType
{
    public class AddEmployeeCommendHandler : IRequestHandler<AddEmployeeCommand, EmployeeTypes>
    {
        private readonly IRepository<EmployeeTypes> _EmployeeRepository;
        private readonly ILogger<AddEmployeeCommendHandler> _logger;
        public AddEmployeeCommendHandler(IRepository<EmployeeTypes> employeeRepository, ILogger<AddEmployeeCommendHandler> logger)
        {
            _EmployeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<EmployeeTypes> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = new EmployeeTypes();
            _logger.LogInformation($"Start AddEmployeeCommandHandler");

            if (request.EmployeeTypes != null)
            {
                var EmpType = new EmployeeTypes
                {
                    EmployeeTypeName = request.EmployeeTypes.EmployeeTypeName,
                    IsActive = request.EmployeeTypes.IsActive,
                    ModifiedBy = null,
                    ModifiedDateUTC = null
                };

                var getEmployeeType = await _EmployeeRepository.Get().Where(x => x.EmployeeTypeName == EmpType.EmployeeTypeName && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);

                if (getEmployeeType == null)
                {
                    var result = await _EmployeeRepository.CreateAsync(EmpType, cancellationToken);
                    if (result.EmployeeTypeId == 0)
                        throw new FCCException(ErrorMessage.EMPLOYEETYPE_SAVE_ERROR);
                    response.EmployeeTypeId = result.EmployeeTypeId;
                }
                else
                    response.EmployeeTypeId = Convert.ToInt32(ResponseEnum.NotExists);

                _logger.LogInformation($"END AddEmployeeCommandHandler");
                return response;
            }
            else
            {
                return response;
            }
        }
    }
}
