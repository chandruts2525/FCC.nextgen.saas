using FCC.Core;
using FCC.Core.Constants;
using FCC.Core.ViewModel.GridFilter;
using IAC.Service.ViewModel.Role;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StackExchange.Redis;
using System.Drawing.Printing;
using WorkManagement.Domain.Model;
using WorkManagement.Service.Commands.EmployeeType;
using WorkManagement.Service.Queries.EmployeeType;

namespace WorkManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : BaseController<EmployeeController>
	{
		private readonly IMediator _mediator;
		private readonly ILogger<EmployeeController> _logger;
		public EmployeeController(IMediator mediator, ILogger<EmployeeController> logger) : base(logger)
		{
			_mediator = mediator;
			_logger = logger;
		}
		/// <summary>
		/// This method enables the caller to GetAll<see cref="EmployeeTypes"/> with Filter
		/// </summary>
		/// <param name="employeeTypes"><see cref="EmployeeTypes"/></param>
		/// <returns>List of EmployeeTypes model</returns>
		[HttpPost("Search")]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Search(List<ColumnFilter>? gridFilterViewModels, string? SearchFilter, int? PageNumber, int? PageSize, string? OrderBy, string? SortOrderBy)
		{
			try
			{
				_logger.LogInformation("GetAllEmployeeStart, Params: {gridFilterViewModels},SearchFilter: {SearchFilter} PageNumber: {PageNumber},pageSize: {PageSize},OrderBy :{OrderBy},SortOrderBy: {SortOrderBy}", gridFilterViewModels, SearchFilter, PageNumber, PageSize, OrderBy, SortOrderBy);
				var result = await _mediator.Send(new GetAllEmployeeQuery(gridFilterViewModels, SearchFilter, PageNumber, PageSize, OrderBy, SortOrderBy));
				if (result != null)
				{
                     int rescount = result.Count;
                    _logger.LogInformation($"GetAllEmployee Complete PageSize = new Se. Params - Count:",rescount);
					return Ok(result);
				}
				return StatusCode(500, ErrorMessage.DATABASE_ERROR);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in EmployeeController.GetAllEmployeeTypes");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to create a <see cref="EmployeeTypes"/>
		/// </summary>
		/// <param name="employeeTypes"><see cref="EmployeeTypes"/></param>
		/// <returns>Message</returns>
		[HttpPost()]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(EmployeeTypes employeeTypes)
		{
			try
			{
				if (!string.IsNullOrEmpty(employeeTypes.EmployeeTypeName))
				{
					_logger.LogInformation($"AddEmployeeType Start");
					var result = await _mediator.Send(new AddEmployeeCommand(employeeTypes));
					_logger.LogInformation($"AddEmployeeType END");
					if (result != null)
					{
						if (result.EmployeeTypeId == Convert.ToInt32(ResponseEnum.NotExists))
							return StatusCode(203, ErrorMessage.EMPLOYEETYPE_ALREADY_EXIST_ERROR);
						return Ok(result);
					}
					return StatusCode(500, ErrorMessage.DATABASE_ERROR);
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in EmployeeController.CreateEmployee");
				return StatusCode(500, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in EmployeeController.CreateEmployee");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to Update a <see cref="EmployeeTypes"/>
		/// </summary>
		/// <param name="employeeTypes"><see cref="EmployeeTypes"/></param>
		/// <returns>Message</returns>
		[HttpPut()]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(EmployeeTypes employeeTypes)
		{
			try
			{
				if (employeeTypes != null && employeeTypes.EmployeeTypeId > 0 && !string.IsNullOrEmpty(employeeTypes.EmployeeTypeName))
				{
					_logger.LogInformation($"updateEmployeeType Start");
					var result = await _mediator.Send(new UpdateEmployeeTypeCommand(employeeTypes));
					_logger.LogInformation($"updateEmployeeType End");
					if (result != null)
					{
						if (result.EmployeeTypeId == Convert.ToInt32(ResponseEnum.NotExists))
							return StatusCode(203, ErrorMessage.EMPLOYEETYPE_ALREADY_EXIST_ERROR);
						return Ok(result);
					}
					return StatusCode(500, ErrorMessage.DATABASE_ERROR);
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in EmployeeController.UpdateEmployee");
				return StatusCode(500, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in EmployeeController.UpdateEmployee");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to GetAll<see cref="EmployeeTypes"/>
		/// </summary>
		/// <param name="employeeTypes"><see cref="EmployeeTypes"/></param>
		/// <returns>EmployeeTypes model</returns>
		[HttpGet("Export")]
		//[ValidateAntiForgeryToken]  
		public async Task<ActionResult> GetExportExcel()
		{
			try
			{
				_logger.LogInformation($"GetAllEmployeesList Start.");
				var result = await _mediator.Send(new GetEmployeeTypesExportExcelQuery());
				_logger.LogInformation($"GetAllEmployeesList Complete.");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in EmployeeController.GetExportExcel");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to EmployeeDeactivate<see cref="EmployeeTypes"/>
		/// </summary>
		/// <param name="EmployeeTypeId"></param>
		/// <param name="IsActive"></param>
		/// <returns>EmployeTypes models</returns>
		[HttpPut("Status")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Status(int EmployeeTypeId, bool IsActive)
		{
			try
			{
				EmployeeTypes employeeTypes = new()
				{
					EmployeeTypeId = EmployeeTypeId,
					IsActive = IsActive,
				};
				var result = await _mediator.Send(new DeactivateEmployeeCommand(employeeTypes));
				return Ok(result.EmployeeTypeName);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in EmployeeController.Status");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}
	}
}
