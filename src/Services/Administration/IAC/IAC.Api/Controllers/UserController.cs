using FCC.Core;
using FCC.Core.Constants;
using FCC.Core.ViewModel.GridFilter;
using IAC.Domain.Model;
using IAC.Domain.ViewModel.SecurityUser;
using IAC.Service.Commands.User;
using IAC.Service.Queries.User;
using IAC.Service.ViewModel.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IAC.Api.Controllers
{
	/// <summary>
	/// Users API controller
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : BaseController<UserController>
	{
		private readonly IMediator _mediator;
		private readonly ILogger<UserController> _logger;

		/// <summary>
		/// Users API controller constructor
		/// </summary>
		/// <param name="mediator"></param>
		/// /// <param name="logger"></param>
		public UserController(IMediator mediator, ILogger<UserController> logger) : base(logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		/// <summary>
		/// Get users list 
		/// </summary>
		/// <returns>SecurityUserResponseViewModel models</returns>
		[HttpPost("Search")]
		//  [ValidateAntiForgeryToken]
		public async Task<IActionResult> Search(List<ColumnFilter>? gridFilterViewModels, string? SearchFilter, int? PageNumber, int? PageSize, string? OrderBy, string? SortOrderBy)
		{
			try
			{
				_logger.LogInformation($"GetAllSecurityUser Start, Params: {gridFilterViewModels},SearchFilter: {SearchFilter} PageNumber: {PageNumber},pageSize: {PageSize},OrderBy :{OrderBy},SortOrderBy: {SortOrderBy}");

				var result = await _mediator.Send(new GetSecurityUserListQuery(gridFilterViewModels, SearchFilter, PageNumber, PageSize, OrderBy, SortOrderBy));

				if (result != null)
				{
					_logger.LogInformation($"GetAllSecurityUser CompletPageSize = new Se. Params - Count: {result.Count}");
					return Ok(result);
				}
				return StatusCode(500, ErrorMessage.DATABASE_ERROR);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UserController.GetAllSecurityUser");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// Create user 
		/// </summary>
		/// <param name="userViewModel"></param>
		/// <returns>User Id</returns>
		[HttpPost()]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateSecurityUserViewModel userViewModel)
		{
			try
			{
				if (userViewModel != null)
				{
					_logger.LogInformation($"CreateUser Start, Params: User: {userViewModel.LoginEmail}, Name: {userViewModel.FirstName}");

					var result = await _mediator.Send(new CreateUserCommand(userViewModel));
					if (result != null)
					{
						_logger.LogInformation($"CreateUser Complete. Params - User: {result.LoginEmail}");
						if (result.UserId == Convert.ToInt32(ResponseEnum.NotExists))
							return StatusCode(409, ErrorMessage.USER_ALREADY_EXIST_ERROR_1010);
						return Ok(result);
					}
					else
						return StatusCode(500, ErrorMessage.UNKNOWN_ERROR);
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in UserController.CreateUser");
				return StatusCode(500, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UserController.CreateUser");

				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// Update user
		/// </summary>
		/// <param name="userViewModel"></param>
		/// <returns>User Id</returns>
		[HttpPut()]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UpdateSecurityUserViewModel userViewModel)
		{
			try
			{
				_logger.LogInformation($"EditUser Start, Params: User: {userViewModel.LoginEmail}, Name: {userViewModel.FirstName}");

				var result = await _mediator.Send(new UpdateUserCommand(userViewModel));
				if (result != null)
				{
					if (result.UserId == Convert.ToInt32(ResponseEnum.NotExists))
						return StatusCode(409, ErrorMessage.USER_ALREADY_EXIST_ERROR_1010);
					_logger.LogInformation($"EditUser Complete. Params - User: {result.LoginEmail}");
					return Ok(result);
				}
				return StatusCode(500, ErrorMessage.DATABASE_ERROR);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in UserController.EditUser");
				return StatusCode(500, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UserController.EditUser");

				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// Get user details by user id 
		/// </summary>
		/// <param name="id"></param>
		/// <returns>GetUserResponseViewModel model</returns>
		[HttpGet("{id}")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> GetById(int id)
		{
			try
			{
				if (id > 0)
				{
					GetUserResponseVM userViewModel = new GetUserResponseVM()
					{
						UserId = id,
					};

					_logger.LogInformation($"GetUserById Start, Params: UserId: {userViewModel.UserId}");
					var result = await _mediator.Send(new GetUserQuery(userViewModel));
					if (result != null)
					{
						_logger.LogInformation($"GetUserById Complete. Params - User Count: {result}");
						return Ok(result);
					}
					return StatusCode(500, ErrorMessage.DATABASE_ERROR);
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UserController.GetUserById");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// Deactive user
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="Status"></param>
		/// <returns>SecurityUser models</returns>
		[HttpPut("Status")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Status(int userId, bool Status)
		{
			try
			{
				SecurityUser securityUser = new()
				{
					UserId = userId,
					Status = Status
				};
				var result = await _mediator.Send(new DeactivateUserCommand(securityUser));
				return Ok(result.FirstName + " " + result.LastName);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UserController.Status");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// Get business entity 
		/// </summary>
		/// <returns>BusinessEntityResponseModel models</returns>
		[HttpGet("GetBusinessEntity")]
		public async Task<ActionResult> GetBusinessEntity()
		{
			try
			{
				BusinessEntityResponseModel businessEntityViewModel = new();
				_logger.LogInformation($"GetBusinessEntity Start.");
				var result = await _mediator.Send(new GetBusinessEntityQuery(businessEntityViewModel));
				_logger.LogInformation($"GetBusinessEntity Complete.");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UserController.GetBusinessEntity");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}
	}
}
