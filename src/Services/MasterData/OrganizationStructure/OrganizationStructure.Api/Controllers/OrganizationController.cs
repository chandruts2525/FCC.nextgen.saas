using FCC.Core;
using FCC.Core.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrganizationManagementService.Service.ViewModel.Organization;
using OrganizationStructure.Service.Commands.Organizations;
using OrganizationStructure.Service.Queries.Organizations;

namespace OrganizationStructure.Api.Controllers
{
    /// <summary>
	/// Organization API controller
	/// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrganizationController : BaseController<OrganizationController>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrganizationController> _logger;

        /// <summary>
		/// Organization API controller constructor
		/// </summary>
		/// <param name="mediator"></param>
		/// /// <param name="logger"></param>
        public OrganizationController(IMediator mediator, ILogger<OrganizationController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Create Organization 
        /// </summary>
        /// <param name="OrganizationVM"></param>
        /// <returns>BusinessEntity</returns>
        [HttpPost]
        public async Task<ActionResult> Create(OrganizationVM OrganizationVM)
        {
            try
            {
                if (!string.IsNullOrEmpty(OrganizationVM.BusinessEntityName))
                {
                    _logger.LogInformation("AddOrganization Started for {OrganizationVM.BusinessEntityName}", OrganizationVM.BusinessEntityName);
                    var result = await _mediator.Send(new CreateOrganizationCommand(OrganizationVM));
                    _logger.LogInformation($"AddOrganization END");
                    return Ok(result);
                }
                return BadRequest(ErrorMessage.NO_PARAMETERS);
            }
            catch (FCCException fccEx)
            {
                _logger.LogError(fccEx, "Error in OrganizationController.Create");
                return StatusCode(500, fccEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OrganizationController.Create");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
		/// Update Organization
		/// </summary>
		/// <param name="OrganizationVM"></param>
		/// <returns>BusinessEntity</returns>
        [HttpPut]
        public async Task<ActionResult> Edit(OrganizationVM OrganizationVM)
        {
            try
            {
                if (OrganizationVM.BusinessEntityID > 0 && !string.IsNullOrEmpty(OrganizationVM.BusinessEntityName))
                {
                    _logger.LogInformation("Edit Organization Started for {OrganizationVM.BusinessEntityName}", OrganizationVM.BusinessEntityName);
                    var result = await _mediator.Send(new UpdateOrganizationCommand(OrganizationVM));
                    _logger.LogInformation($"Edit Organization End");
                     if (result.BusinessEntityID == Convert.ToInt32(ResponseEnum.NotExists))
                            return StatusCode(409, ErrorMessage.NOT_EXISTS);
                     return Ok(result);
                    
                }
                return BadRequest(ErrorMessage.NO_PARAMETERS);
            }
            catch (FCCException fccEx)
            {
                _logger.LogError(fccEx, "Error in OrganizationController.Edit");
                return StatusCode(500, fccEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OrganizationController.Edit");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
		/// Get Organization
		/// </summary>
		/// <returns>BusinessEntity models</returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                OrganizationVM organizationVM = new();
                _logger.LogInformation($"Get Start.");
                var result = await _mediator.Send(new GetOrganizationQuery(organizationVM));
                _logger.LogInformation($"Get Complete.");
                if (result.BusinessEntityID != 0)
                {
                    return Ok(result);
                }
                return Ok("Organization not Found");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OrganizationController.Get");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }
    }
}

