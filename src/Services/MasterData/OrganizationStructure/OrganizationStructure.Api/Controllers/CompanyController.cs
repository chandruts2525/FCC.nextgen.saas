using FCC.Core;
using FCC.Core.Constants; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OrganizationStructure.Domain.ViewModel.Company;
using OrganizationStructure.Service.Commands.Company;
using IAC.Service.Commands.Role;

namespace OrganizationStructure.Api.Controllers
{
    /// <summary>
	/// Companys API controller
	/// </summary>
	[Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController<CompanyController>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CompanyController> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Companys API controller constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// /// <param name="logger"></param>
        /// /// <param name="configuration"></param>
        public CompanyController(IMediator mediator, ILogger<CompanyController> logger, IConfiguration configuration) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
            _configuration = configuration;
        } 

        /// <summary>
		/// Create Company Logo 
		/// </summary>
		/// <param name="Files"></param>
		/// <returns>FileReturnData models</returns>
		[HttpPost("CreateCompanyLogo")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCompanyLogo([FromForm] List<IFormFile> Files)
        {
            try
            {
                if (Files.Count > 0)
                {
                    _logger.LogInformation($"CreateCompanyLogo Start");
                    string ContainerName = _configuration.GetValue<string>("BlobContainerName");
                    string ContainerFolderName = _configuration.GetValue<string>("BlobContainerFolderName");
                    var result = await _mediator.Send(new CreateAttachmentCommand(Files, ContainerName, ContainerFolderName));
                    _logger.LogInformation($"CreateCompanyLogo End");
                    return Ok(result);
                }
                return StatusCode(400, ErrorMessage.NO_PARAMETERS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CompanyController.CreateCompanyLogo");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);

            }
        }

        /// <summary>
		/// This method enables the caller to create a <see cref="SecurityCompany"/> 
		/// </summary>
		/// <param name="createCompanyVM"><see cref="SecurityCompany"/></param>
		/// <returns>Company Id</returns>
		[HttpPost()]
        public async Task<IActionResult> Create(CreateCompanyVM? createCompanyVM)
        {
            try
            {
                if (createCompanyVM != null)
                {
                    _logger.LogInformation($"CreateCompany Start, Params: Company: {createCompanyVM}");
                    var result = await _mediator.Send(new CreateCompanyCommand(createCompanyVM));
                    if (result != null)
                    {
                        _logger.LogInformation($"CreateCompany Complete. Params - Company: {result}");
                        if (!result.IsSuccessful)
                            return StatusCode(409, result.ErrorMessage);
                        return Ok(result);
                    }
                    else
                        return StatusCode(500, ErrorMessage.UNKNOWN_ERROR);
                }
                return BadRequest(ErrorMessage.NO_PARAMETERS);
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CompanyController.CreateCompany");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

    }
}
