using FCC.Core;
using FCC.Core.Constants;
using FCC.Core.ViewModel.GridFilter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkManagement.Domain.Model;
using WorkManagement.Service.Commands.JobType;
using WorkManagement.Service.Queries.JobType;

namespace WorkManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class JobController : BaseController<JobController>
	{
		private readonly IMediator _mediator;
		private readonly ILogger<JobController> _logger;

		public JobController(IMediator mediator, ILogger<JobController> logger) : base(logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		/// <summary>
		/// This method enables the caller to GetAll <see cref="JobTypes"/> with Filter
		/// </summary>
		/// <param name="JobTypes"><see cref="JobTypes"/></param>
		/// <returns>List Of JobTypes Model</returns>
		[HttpPost("Search")]
		//  [ValidateAntiForgeryToken]
		public async Task<IActionResult> Search(List<ColumnFilter>? gridFilterViewModels, string? SearchFilter, int? PageNumber, int? PageSize, string? OrderBy, string? SortOrderBy)
		{
			try
			{
				_logger.LogInformation("GetJobTypeList Start, Params: {gridFilterViewModels},SearchFilter: {SearchFilter} PageNumber: {PageNumber},pageSize: {PageSize},OrderBy :{OrderBy},SortOrderBy: {SortOrderBy}", gridFilterViewModels, SearchFilter, PageNumber, PageSize, OrderBy, SortOrderBy);
				var result = await _mediator.Send(new GetJobTypeListQuery(gridFilterViewModels, SearchFilter, PageNumber, PageSize, OrderBy, SortOrderBy));
				if (result != null)
				{
                    int rescount = result.Count;
                    _logger.LogInformation($"GetJobTypeList CompletPageSize = new Se. Params - Count:",rescount);
					return Ok(result);
				}
				return StatusCode(500, ErrorMessage.DATABASE_ERROR);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in JobController.GetJobTypeList");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to create a <see cref="JobTypes"/>
		/// </summary>
		/// <param name="JobTypes"><see cref="JobTypes"/></param>
		/// <returns>Message</returns>
		[HttpPost()]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(JobTypes jobType)
		{
			try
			{
				if (!string.IsNullOrEmpty(jobType.JobTypeCode) && !string.IsNullOrEmpty(jobType.JobTypeName))
				{
					_logger.LogInformation($"Job API Start, Params:  Name: {jobType.JobTypeName}");
					var result = await _mediator.Send(new CreateJobTypesCommand(jobType));
					if (result != null)
					{
						if (result.JobTypeId == Convert.ToInt32(ResponseEnum.NotExists))
							return StatusCode(203, ErrorMessage.JOBTYPE_ALREADY_EXIST_ERROR);
						return Ok(result);
					}
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in JobController.CreateJobType");
				return StatusCode(500, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in JobController.CreateJobType");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to Update a <see cref="JobTypes"/>
		/// </summary>
		/// <param name="JobTypes"><see cref="JobTypes"/></param>
		/// <returns>Message</returns>
		[HttpPut()]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(JobTypes jobType)
		{
			try
			{
				if (jobType.JobTypeCode != null)
				{
					_logger.LogInformation($"UpdateJobType API Start, Params:  Name: {jobType.JobTypeName}");
					var result = await _mediator.Send(new UpdateDeleteJobTypesCommand(jobType));
					if (result != null)
					{
						if (result.JobTypeId == Convert.ToInt32(ResponseEnum.NotExists))
							return StatusCode(203, ErrorMessage.JOBTYPE_ALREADY_EXIST_ERROR);
						return Ok(result);
					}
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in JobController.UpdateJobType");
				return StatusCode(500, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in JobController.UpdateJobType");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to GetAll <see cref="JobTypes"/>
		/// </summary>
		/// <param name="JobTypes"><see cref="JobTypes"/></param>
		/// <returns>List Of JobTypes Model</returns>
		[HttpGet("Export")]
		public async Task<ActionResult> GetExportExcel()
		{
			try
			{
				_logger.LogInformation($"GetAllJobTypes Start.");
				var result = await _mediator.Send(new GetAllJobTypesQuery());
				_logger.LogInformation($"GetAllJobTypes Complete.");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in JobController.GetAllJobTypes");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		///  This method enables the caller to JobTypeDeactivate <see cref="JobTypes"/>
		/// </summary>
		/// <param name="JobTypeId"></param>
		/// <param name="IsActive"></param>
		/// <returns></returns>
		[HttpPut("Status")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Status(int JobTypeId, bool IsActive)
		{
			try
			{
				JobTypes jobTypes = new()
				{
					JobTypeId = JobTypeId,
					IsActive = IsActive
				};
				var result = await _mediator.Send(new DeactivateJobTypeDeactivateCommand(jobTypes));
				return Ok(result.JobTypeName);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in JobTypesController.JobTypesDeactivate");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}
	}
}