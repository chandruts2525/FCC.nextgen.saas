using FCC.Core;
using FCC.Core.Constants;
using WorkManagement.Service.Commands.QuoteFooter;
using WorkManagement.Service.Queries.QuoteFooter;
using WorkManagement.Service.ViewModel.QuoteFooter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FCC.Core.ViewModel.GridFilter;
using IAC.Service.Commands.QuoteFooter;
using WorkManagement.Service.Queries.JobType;

namespace WorkManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuoteFootersController : BaseController<QuoteFootersController>
	{
		private readonly IMediator _mediator;
		private readonly ILogger<QuoteFootersController> _logger;

		public QuoteFootersController(IMediator mediator, ILogger<QuoteFootersController> logger) : base(logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		/// <summary>
		/// This method enables the caller to GetAll <see cref="QuoteFooters"/> List
		/// </summary>
		/// <param name="gridFilterViewModel"><see cref="GridFilterViewModel"/></param>
		/// <returns>List Of QuoteFooters List</returns>
		[HttpPost("Search")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Search(List<ColumnFilter>? gridFilterViewModel, string? SearchFilter, int? PageNumber, int? PageSize, string? OrderBy, string? SortOrderBy)
		{
			try
			{
				_logger.LogInformation("GetAllQuoteFooters Start, Params: {gridFilterViewModel},SearchFilter: {SearchFilter} PageNumber: {PageNumber},pageSize: {PageSize},OrderBy :{OrderBy},SortOrderBy: {SortOrderBy}", gridFilterViewModel, SearchFilter, PageNumber, PageSize, OrderBy, SortOrderBy);
				var result = await _mediator.Send(new GetAllQuoteFooterQuery(gridFilterViewModel, SearchFilter, PageNumber, PageSize, OrderBy, SortOrderBy));
				if (result != null)
				{
                    int rescount = result.Count;
                    _logger.LogInformation($"GetAllQuoteFooters CompletPageSize = new Se. Params - Count:",rescount);
					return Ok(result);
				}
				return StatusCode(500, ErrorMessage.DATABASE_ERROR);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in QuoteFootersController.GetAllQuoteFooters");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to create a <see cref="QuoteFooters"/>
		/// </summary>
		/// <param name="quoteFooters"><see cref="QuoteFooters"/></param>
		/// <returns>Message</returns>
		[HttpPost()]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(QuoteFootersRequestVM quoteFooters)
		{
			try
			{
				if (quoteFooters != null)
				{
					_logger.LogInformation($"Add QuoteFooters Start");
					var result = await _mediator.Send(new CreateQuoteFootersCommand(quoteFooters));
					_logger.LogInformation($"Add QuoteFooters End");
					if (result != null)
					{
						if (result.QuoteFootersID == Convert.ToInt32(ResponseEnum.NotExists))
							return StatusCode(203, ErrorMessage.QUOTE_FOOTER_ALREADY_EXIST_ERROR);
						return Ok(result);
					}
					return StatusCode(500, ErrorMessage.DATABASE_ERROR);
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in QuoteFootersController.CreateQuoteFooters");
				return StatusCode(500, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in QuoteFootersController.CreateQuoteFooters");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}
		/// <summary>
		/// This method enables the caller to update a <see cref="QuoteFooters"/>
		/// </summary>
		/// <param name="quoteFooters"><see cref="QuoteFooters"/></param>
		/// <returns></returns>
		[HttpPut()]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(QuoteFootersRequestVM quoteFooters)
		{
			try
			{
				if (quoteFooters.QuoteFootersID != 0)
				{
					_logger.LogInformation($"Update QuoteFooters Start");
					var result = await _mediator.Send(new UpdateQuoteFootersCommand(quoteFooters));
					_logger.LogInformation($"Update Quote Footers End");
					if (result != null)
					{
						if (result.QuoteFootersID == Convert.ToInt32(ResponseEnum.NotExists))
							return StatusCode(203, ErrorMessage.QUOTE_FOOTER_ALREADY_EXIST_ERROR);
						return Ok(result);
					}
					return StatusCode(500, ErrorMessage.DATABASE_ERROR);
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in QuoteFootersController.UpdateQuoteFooters");
				return StatusCode(500, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in QuoteFootersController.UpdateQuoteFooters");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to Get a <see cref="QuoteFooters"/> By ID
		/// </summary>
		/// <param name="id"><see cref="QuoteFooters"/></param>
		/// <returns>QuoteFooter Model</returns>
		[HttpGet("{id}")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> GetByID(int id)
		{
			try
			{
				if (id > 0)
				{
					_logger.LogInformation($"GetById QuoteFooters Start");
					var result = await _mediator.Send(new GetQuoteFooterByIDQuery(id));
					_logger.LogInformation($"GetById Quote Footers End");
					if (result.QuoteFooterId != null)
						return Ok(result);
					return NoContent();
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in QuoteFootersController.GetBYIdQuoteFooters");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to Get Company Dropdown
		/// </summary>
		/// <returns>List of Company DropDown</returns>
		[HttpGet("GetCompany")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> GetCompany()
		{
			try
			{
				_logger.LogInformation($"GetCompany Start.");
				var result = await _mediator.Send(new GetCompanyQuery());
				if (result != null)
				{
					_logger.LogInformation($"GetCompany END.");
					return Ok(result);
				}
				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in QuoteFootersController.GetCompany");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to get Modules List
		/// </summary>
		/// <returns>List Of Modules</returns>
		[HttpGet("GetModule")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> GetModule()
		{
			try
			{
				_logger.LogInformation($"GetModule Start.");
				var result = await _mediator.Send(new GetModuleQuery());
				if (result != null)
				{
					_logger.LogInformation($"GetModule Complete.");
					return Ok(result);
				}
				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in QuoteFootersController.GetModule");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// This method enables the caller to Delete the <see cref="QuoteFooters"/>
		/// </summary>
		/// <param name="quoteFooters"><see cref="QuoteFooters"/></param>
		/// <returns></returns>
		[HttpDelete()]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int QuoteFooterId)
		{
			try
			{
				if (QuoteFooterId != 0)
				{
					_logger.LogInformation($"DeleteQuoteFooter Start.");
					var result = await _mediator.Send(new DeleteQuoteFooterCommand(QuoteFooterId));
					if (result != null)
					{
						if (result == -1)
							return BadRequest(ErrorMessage.INVALID_PARAMETERS);
						return Ok("Deleted Successfully");
					}
					return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
				}
				return BadRequest(ErrorMessage.INVALID_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in QuoteFootersController.DeleteQuoteFooters");
				return StatusCode(500, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in QuoteFootersController.DeleteQuoteFooter");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		[HttpPut("Status")]
		public async Task<ActionResult> Status(int QuoteFooterId, bool Status)
		{
			try
			{
				if (QuoteFooterId != 0)
				{
					QuoteFootersRequestVM quoteFootersRequestViewModel = new QuoteFootersRequestVM();
					quoteFootersRequestViewModel.QuoteFootersID = QuoteFooterId;
					quoteFootersRequestViewModel.Status = Status;
					_logger.LogInformation($"ActivateDeactivateQuoteFooter Start.");
					var result = await _mediator.Send(new ActivateDeactivateQuoteFooterCommand(quoteFootersRequestViewModel));
					if (result.TermID > 0)
						return Ok(result);
					else
						return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
				}
				return BadRequest(ErrorMessage.INVALID_PARAMETERS);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in QuoteFootersController.ActivateDeactivateQuoteFooter");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}
	}
}
