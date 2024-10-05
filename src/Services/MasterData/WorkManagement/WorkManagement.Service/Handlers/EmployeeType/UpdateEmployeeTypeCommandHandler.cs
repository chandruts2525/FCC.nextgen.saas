using FCC.Core;
using FCC.Core.Constants;
using WorkManagement.Domain.Model;
using WorkManagement.Service.Commands.EmployeeType;
using WorkManagement.Service.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using Softura.EntityFrameworkCore.Abstractions;

namespace WorkManagement.Service.Handlers.EmployeeType
{
	public class UpdateEmployeeTypeCommandHandler : IRequestHandler<UpdateEmployeeTypeCommand, EmployeeTypes>
	{
		private readonly IRepository<EmployeeTypes> _EmployeeRepository;
		private readonly ILogger<UpdateEmployeeTypeCommandHandler> _logger;

		public UpdateEmployeeTypeCommandHandler(IRepository<EmployeeTypes> employeeRepository, ILogger<UpdateEmployeeTypeCommandHandler> logger)
		{
			_EmployeeRepository = employeeRepository;
			_logger = logger;
		}

		public async Task<EmployeeTypes> Handle(UpdateEmployeeTypeCommand request, CancellationToken cancellationToken)
		{
			var response = new EmployeeTypes();
			_logger.LogInformation($"Start UpdateEmployeeTypeCommandHandler");

			if (request.EmployeeTypes.EmployeeTypeId > 0)
			{
				var EmpUpdate = new EmployeeTypes
				{
					EmployeeTypeId = request.EmployeeTypes.EmployeeTypeId,
					EmployeeTypeName = request.EmployeeTypes.EmployeeTypeName,
					IsActive = request.EmployeeTypes.IsActive,
					IsDeleted = request.EmployeeTypes.IsDeleted,
					ModifiedBy = "Administrator",
					ModifiedDateUTC = DateTime.UtcNow
				};

				var getEmployeeType = await _EmployeeRepository.Get().Where(x => x.EmployeeTypeName == EmpUpdate.EmployeeTypeName && x.EmployeeTypeId != EmpUpdate.EmployeeTypeId
				&& !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);

				if (getEmployeeType == null)
				{
					var result = await _EmployeeRepository.UpdateAsync(EmpUpdate, cancellationToken);
					if (result.EmployeeTypeId == 0)
						throw new FCCException(ErrorMessage.EMPLOYEETYPE_UPDATE_ERROR);
					response.EmployeeTypeId = result.EmployeeTypeId;
				}
				else
					response.EmployeeTypeId = Convert.ToInt32(ResponseEnum.NotExists);

				_logger.LogInformation($"END UpdateEmployeeTypeCommandHandler");
				return response;
			}
			else
			{
				return response;
			}
		}
	}
}
