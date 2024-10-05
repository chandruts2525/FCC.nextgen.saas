using FCC.Core;
using FCC.Core.Constants;
using FCC.Core.ViewModel.GridFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkManagement.Domain.ViewModel.ScheduleTypes;
using WorkManagement.Service.Commands.ScheduleType;
using WorkManagement.Service.Queries.SchdeuleType;
using WorkManagement.Service.ViewModel.ScheduleType;

namespace WorkManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ScheduleTypeController : BaseController<ScheduleTypeController>
	{
		private readonly IMediator _mediator;
		private readonly ILogger<ScheduleTypeController> _logger;
		public ScheduleTypeController(IMediator mediator, ILogger<ScheduleTypeController> logger, IConfiguration configuration) : base(logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		/// <summary>
		/// This method enables the caller to GetAll <see cref="ScheduleTypes"/> with Filter
		/// </summary>
		/// <param name="ScheduleTypes"><see cref="ScheduleTypes"/></param>
		/// <returns>List Of ScheduleTypes Model</returns>
		[HttpPost("Search")]
		//  [ValidateAntiForgeryToken]
		public async Task<IActionResult> Search(List<ColumnFilter>? gridFilterViewModels, string? SearchFilter, int? PageNumber, int? PageSize, string? OrderBy, string? SortOrderBy)
		{
			try
			{
				_logger.LogInformation("GetScheduleTypeList Start, Params: {gridFilterViewModels},SearchFilter: {SearchFilter} PageNumber: {PageNumber},pageSize: {PageSize},OrderBy :{OrderBy},SortOrderBy: {SortOrderBy}", gridFilterViewModels, SearchFilter, PageNumber, PageSize, OrderBy, SortOrderBy);

				var result = await _mediator.Send(new GetScheduleTypeListQuery(gridFilterViewModels, SearchFilter, PageNumber, PageSize, OrderBy, SortOrderBy));
				if (result != null)
				{
					_logger.LogInformation("GetScheduleTypeList CompletPageSize = new Se. Params - Count: {result.Count}", result.Count);
					return Ok(result);
				}
				return StatusCode(500, ErrorMessage.DATABASE_ERROR);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in ScheduleTypeController.GetScheduleTypeList");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to GetAll <see cref="ScheduleTypes"/> with Filter
		/// </summary>
		/// <param name="ScheduleTypes"><see cref="ScheduleTypes"/></param>
		/// <returns>List Of ScheduleTypes Model</returns>
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(ScheduleTypeViewModel scheduleTypeViewModel)
		{
			try
			{
				if (scheduleTypeViewModel != null)
				{
					_logger.LogInformation($"Create Schedule Type Start, Params: Schedule Type: {scheduleTypeViewModel.ScheduleTypeCode}, CreatedBy: {scheduleTypeViewModel.CreatedBy}");
					var result = await _mediator.Send(new AddScheduleTypeCommand(scheduleTypeViewModel));
					if (result != null)
					{
						_logger.LogInformation($"Successfully Added New Schedule Type");
						return Ok(result.ScheduleTypeCode);
					}
					else
					{
						return StatusCode(500, ErrorMessage.DATABASE_ERROR);
					}
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in ScheduleTypeController.AddScheduleType");
				return StatusCode(203, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in ScheduleTypeController.AddSchedule");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to Update a <see cref="ScheduleTypes"/>
		/// </summary>
		/// <param name="ScheduleTypes"><see cref="ScheduleTypes"/></param>
		/// <returns>Message</returns>
		[HttpPut]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(ScheduleTypeViewModel scheduleTypeViewModel)
		{
			try
			{
				if (scheduleTypeViewModel != null)
				{
					if (scheduleTypeViewModel.ScheduleTypeId > 0)
					{
						var result = await _mediator.Send(new UpdateScheduleTypeCommand(scheduleTypeViewModel));
						if (result != null)
						{
							_logger.LogInformation($"ScheduleType Complete. Params - Role: {result.ScheduleTypeId}");
							return Ok(result.ScheduleTypeId);
						}
					}
					else
					{
						return StatusCode(203, ErrorMessage.NONEDITABLE);
					}
				}
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in ScheduleTypeController.UpdateScheduleType");
				return StatusCode(203, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in ScheduleTypeController.UpdateScheduleType");

				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
			return BadRequest(ErrorMessage.NO_PARAMETERS);
		}

		/// <summary>
		/// This method enables the caller to GetAll <see cref="ScheduleType"/>
		/// </summary>
		/// <param name="ScheduleType"><see cref="ScheduleType"/></param>
		/// <returns>List Of UnitOrComponent Model</returns>
		[HttpGet("GetUnitorComponent")]
		public async Task<ActionResult> GetUnitorComponent()
		{
			try
			{
				UnitorComponentVM unitorComponentViewModel = new();
				_logger.LogInformation($"GetUnitorComponent Start.");
				var result = await _mediator.Send(new GetUnitorComponentQuery(unitorComponentViewModel));
				_logger.LogInformation($"GetUnitorComponent Complete.");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in ScheduleTypeController.GetUnitorComponent");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to ScheduleTypeDeactivate<see cref="ScheduleType"/>
		/// </summary>
		/// <param name="ScheduleTypeId"></param>
		/// <param name="IsActive"></param>
		/// <returns>Message</returns>
		[HttpPut("Status")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Status(int ScheduleTypeId, bool status)
		{
			try
			{
				if (ScheduleTypeId != 0)
				{
					_logger.LogInformation($"ScheduleTypeActivateDeactivate Start.");
					var result = await _mediator.Send(new ScheduleTypeDeactivateCommand(ScheduleTypeId, status));
					_logger.LogInformation($"ScheduleTypeActivateDeactivate Complete.");
					if (result != null)
					{
						if (result.ScheduleTypeId == -1)
							return BadRequest(ErrorMessage.INVALID_PARAMETERS);
						return Ok(result);
					}
					return StatusCode(500, ErrorMessage.DATABASE_ERROR);
				}
				return BadRequest(ErrorMessage.INVALID_PARAMETERS);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in ScheduleTypeController.ScheduleTypeActivateDeactivate");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}
	}
}
