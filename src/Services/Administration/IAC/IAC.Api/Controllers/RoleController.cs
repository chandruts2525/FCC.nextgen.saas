using FCC.Core;
using FCC.Core.Constants;
using IAC.Domain.Model;
using IAC.Domain.ViewModel.Role;
using IAC.Service.Commands.Role;
using IAC.Service.Queries.Role;
using IAC.Service.ViewModel.Role;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IAC.Api.Controllers
{
	/// <summary>
	/// Roles API controller
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : BaseController<RoleController>
	{
		private readonly IMediator _mediator;
		private readonly ILogger<RoleController> _logger;
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Roles API controller constructor
		/// </summary>
		/// <param name="mediator"></param>
		/// /// <param name="logger"></param>
		/// /// <param name="configuration"></param>
		public RoleController(IMediator mediator, ILogger<RoleController> logger, IConfiguration configuration) : base(logger)
		{
			_mediator = mediator;
			_logger = logger;
			_configuration = configuration;
		}

		/// <summary>
		/// Get roles list based on search
		/// </summary>
		/// <param name="roleName"></param>
		/// <param name="userCount"></param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="orderBy"></param>
		/// <param name="sortOrder"></param>
		/// <returns>SearchRoleResponseVM models</returns>
		[HttpGet("Search")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> Search(string? roleName, int? userCount, int? pageNumber, int? pageSize, string? orderBy, string? sortOrder)
		{
			try
			{
				SearchRoleVM roleSearchViewModel = new SearchRoleVM()
				{
					RoleName = roleName ?? "",
					UserCount = userCount ?? 0,
					PageNumber = pageNumber ?? 0,
					PageSize = pageSize ?? 0,
					OrderBy = orderBy ?? "",
					SortOrder = sortOrder ?? ""
				};

				_logger.LogInformation($"SearchRole Start, Params: Role: {roleSearchViewModel.RoleName}, Usercount: {roleSearchViewModel.UserCount}");
				var result = await _mediator.Send(new SearchRoleQuery(roleSearchViewModel));
				if (result != null)
				{
					_logger.LogInformation($"SearchRole Complete.");
					return Ok(result);
				}
				return StatusCode(500, ErrorMessage.DATABASE_ERROR);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in RoleController.Search");
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// This method enables the caller to create a <see cref="SecurityRole"/> 
		/// </summary>
		/// <param name="createRoleVM"><see cref="SecurityRole"/></param>
		/// <returns>Role Id</returns>
		[HttpPost()]
		public async Task<IActionResult> Create(CreateRoleVM? createRoleVM)
		{
			try
			{
				if (createRoleVM != null)
				{
					_logger.LogInformation($"CreateRole Start, Params: Role: {createRoleVM.RoleName}");
					var result = await _mediator.Send(new CreateRoleCommand(createRoleVM));
					if (result != null)
					{
						_logger.LogInformation($"CreateRole Complete. Params - Role: {result.RoleName}");
						if (result.RoleId == Convert.ToInt32(ResponseEnum.NotExists))
							return StatusCode(409, ErrorMessage.ROLE_ALREADY_EXIST_ERROR);
						return Ok(result);
					}
					else
						return StatusCode(500, ErrorMessage.UNKNOWN_ERROR);
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in RoleController.CreateRole");
				return StatusCode(500, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in RoleController.CreateRole");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// Update role
		/// </summary>
		/// <param name="updateRoleVM"></param><see cref="SecurityRole"/></param>
		/// <returns>Role Id</returns>
		[HttpPut()]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UpdateRoleVM? updateRoleVM)
		{
			try
			{
				if (updateRoleVM != null)
				{
					_logger.LogInformation($"EditRole Start, Params: Role: {updateRoleVM.RoleName}");
					var result = await _mediator.Send(new UpdateRoleCommand(updateRoleVM));
					if (result != null)
					{
						_logger.LogInformation($"EditRole Complete. Params - Role: {result.RoleName}");
						if (result.RoleId == -1)
							return StatusCode(409, ErrorMessage.ROLE_ALREADY_EXIST_ERROR);
						return Ok(result);
					}
					else
						return StatusCode(500, ErrorMessage.UNKNOWN_ERROR);
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in RoleController.EditRole");
				return StatusCode(500, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in RoleController.EditRole");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// Get role details by role id 
		/// </summary>
		/// <param name="id"></param>
		/// <returns>GetRoleByIdResponseVM model</returns>
		[HttpGet("{id}")]
		//[ValidateAntiForgeryToken]
		public async Task<ActionResult> GetById(int id)
		{
			try
			{
				if (id > 0)
				{
					GetRoleByIdResponseVM getRoleByIdResponseVM = new GetRoleByIdResponseVM()
					{
						RoleId = id,
					};
					_logger.LogInformation($"GetRole Start, Params: Role: {getRoleByIdResponseVM.RoleId}");
					var result = await _mediator.Send(new GetRoleByIdQuery(getRoleByIdResponseVM));
					if (result != null)
					{
						_logger.LogInformation($"GetRole Complete. Params - Role Count: {result.Count}");
						return Ok(result);
					}
					return StatusCode(500, ErrorMessage.DATABASE_ERROR);
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in RoleController.GetRole");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}


		/// <summary>
		/// Get user list based on business entity
		/// </summary>
		/// <param name="RoleId"></param>
		/// <param name="BusinessEntityTypeIds"></param>
		/// <param name="BusinessEntityIds"></param>
		/// <returns>GetAllUserByBusinessEntityResponseVM models</returns>
		[HttpGet("GetAllUserByBusinessEntity")]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> GetAllUserByBusinessEntity(int? RoleId = 0, string? BusinessEntityTypeIds = "", string? BusinessEntityIds = "")
		{
			try
			{
				GetAllUserByBusinessEntityVM allUserViewModel = new GetAllUserByBusinessEntityVM()
				{
					BusinessEntityIds = BusinessEntityIds == null ? String.Empty : BusinessEntityIds,
					BusinessEntityTypeIds = BusinessEntityTypeIds == null ? String.Empty : BusinessEntityTypeIds,
					RoleId = RoleId
				};
				_logger.LogInformation($"GetAllUser Start, Params:  {allUserViewModel}");
				var result = await _mediator.Send(new GetAllUserByBusinessEntityQuery(allUserViewModel));
				if (result != null)
				{
					if (result.Count == 0)
					{
						return Ok("");
					}
					_logger.LogInformation($"GetAllUser Complete. Params - result: {result}");
					return Ok(result);
				}
				return StatusCode(500, ErrorMessage.DATABASE_ERROR);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in RoleController.GetAllUser");

				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// Get business entity detail by type 
		/// </summary>
		/// <param name="id"></param>
		/// <returns>GetBusinessEntityByTypeResponseVM models</returns>
		[HttpGet("GetBusinessEntityByType/{id}")]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> GetBusinessEntityByType(int id)
		{
			try
			{
				GetBusinessEntityByTypeResponseVM businessEntityViewModel = new()
				{
					BusinessEntityTypeID = id

				};
				var type = await _mediator.Send(new GetBusinessEntityByTypeQuery(businessEntityViewModel));
				if (type is not null)
				{
					return Ok(type);
				}
				return NotFound();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in RoleController.GetBusinessEntityByType");

				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}

		}

		/// <summary>
		/// Get business type list 
		/// </summary>
		/// <returns>GetBusinessEntityByTypeResponseVM models</returns>
		[HttpGet("GetBusinessType")]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> GetBusinessType()
		{
			try
			{
				GetBusinessEntityByTypeResponseVM businessentitymode = new();
				var result = await _mediator.Send(new GetBusinessTypesQuery(businessentitymode));
				if (result != null)
				{
					return Ok(result);
				}
				return StatusCode(500, ErrorMessage.DATABASE_ERROR);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in RoleController.GetBusinessType");

				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}

		/// <summary>
		/// Get attachment 
		/// </summary>
		/// <param name="Files"></param>
		/// <returns>FileReturnData models</returns>
		[HttpGet("Attachment/{id}")]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> GetRoleAttachments(int id)
		{
			try
			{
				if (id != 0)
				{
					_logger.LogInformation($"GetRole Start, Params: Role: {id}");
					var result = await _mediator.Send(new GetRoleAttachmentQuery(id));
					if (result != null)
					{
						return Ok(result);
					}
					return NoContent();
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in RoleController.GetRoleAttachments");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);

			}
		}

		/// <summary>
		/// Create attachment 
		/// </summary>
		/// <param name="Files"></param>
		/// <returns>FileReturnData models</returns>
		[HttpPost("CreateAttachment")]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateAttachment([FromForm] List<IFormFile> Files)
		{
			try
			{
				if (Files.Count > 0)
				{
					_logger.LogInformation($"CreateAttachment Start");
					string ContainerName = _configuration.GetValue<string>("BlobContainerName");
					string ContainerFolderName = _configuration.GetValue<string>("BlobContainerFolderName");
					var result = await _mediator.Send(new CreateAttachmentCommand(Files, ContainerName, ContainerFolderName));
					_logger.LogInformation($"CreateAttachment End");
					return Ok(result);
				}
				return StatusCode(400, ErrorMessage.NO_PARAMETERS);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in RoleController.CreateAttachment");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR); 

			}
		}

		/// <summary>
		/// Download attachment 
		/// </summary>
		/// <param name="FileURI"></param>
		/// <returns>FileReturnData models</returns>
		[HttpGet("DownloadAttachment")]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> DownloadAttachment(string FileURI)
		{
			try
			{
				if (!string.IsNullOrEmpty(FileURI))
				{
					_logger.LogInformation($"DownloadAttachment Start");
					string ContainerName = _configuration.GetValue<string>("BlobContainerName");
					string ContainerFolderName = _configuration.GetValue<string>("BlobContainerFolderName");
					var result = await _mediator.Send(new DownloadAttachmentQuery(FileURI, ContainerName, ContainerFolderName));
					_logger.LogInformation($"DownloadAttachment End");

					if (result != null)
						return Ok(result);
					else
						return Ok("File Not Found");
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in RoleController.DownloadAttachment");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR); 
			}
		}

		/// <summary>
		/// Delete attachment 
		/// </summary>
		/// <param name="FileURI"></param>
		/// <param name="roleAttachmentID"></param>
		/// <returns>FileReturnData models</returns>
		[HttpDelete("DeleteAttachment")]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteAttachment(string FileURI, int? roleAttachmentID)
		{
			try
			{
				if (!string.IsNullOrEmpty(FileURI))
				{
					string ContainerName = _configuration.GetValue<string>("BlobContainerName");
					string ContainerFolderName = _configuration.GetValue<string>("BlobContainerFolderName");
					_logger.LogInformation($"DeleteAttachment Start");
					var result = await _mediator.Send(new DeleteAttachmentCommand(FileURI, roleAttachmentID, ContainerFolderName, ContainerName));
					_logger.LogInformation($"DeleteAttachment End");

					if (result)
						return Ok("File Deleted Successfully");
					else
						return Ok("File Not Found");
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in RoleController.DownloadAttachment");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}
	}
}

