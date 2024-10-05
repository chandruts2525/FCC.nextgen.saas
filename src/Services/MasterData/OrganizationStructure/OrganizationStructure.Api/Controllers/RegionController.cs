using FCC.Core;
using FCC.Core.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationManagementService.Domain.Model;
using OrganizationManagementService.Service.Commands.Region;
using OrganizationStructure.Domain.Model;
using OrganizationStructure.Service.Commands.Organizations;
using OrganizationManagementService.Service.Queries.Region;
using OrganizationManagementService.Service.ViewModel.Region;

namespace OrganizationStructure.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RegionController : BaseController<RegionController>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RegionController> _logger;

        /// <summary>
        /// Region controller constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// /// <param name="logger"></param>
        public RegionController(IMediator mediator, ILogger<RegionController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
		/// Create Region 
		/// </summary>
		/// <param name="RegionRequestVM"></param>
		/// <returns>BusinessEntity</returns>
        [HttpPost]
        public async Task<ActionResult> Create(RegionRequestVM RegionRequestVM)
        {
            try
            {
                if (!string.IsNullOrEmpty(RegionRequestVM.RegionName))
                {
                    _logger.LogInformation($"RegionOrganization Start");
                    var result = await _mediator.Send(new CreateRegionCommand(RegionRequestVM));
                    _logger.LogInformation($"RegionOrganization END");
                    return Ok(result);

                }
                return BadRequest(ErrorMessage.NO_PARAMETERS);
            }
            catch (FCCException fccEx)
            {
                _logger.LogError(fccEx, "Error in RegionController.Create");
                return StatusCode(500, fccEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RegionController.Create");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Edit Region
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RegionRequestVM regionRequestVM)
        {
            try
            {
                if(regionRequestVM.RegionID != null && !string.IsNullOrEmpty(regionRequestVM.RegionName) && regionRequestVM.RegionID > 0)
                {
                    _logger.LogInformation($"EditRegion Start, Params: Region: {regionRequestVM.RegionName}");
                    var result = await _mediator.Send(new UpdateRegionCommand(regionRequestVM));
                    _logger.LogInformation($"EditRegion END ");

                    if (result.RegionID != null)
                    {
                        if (result.RegionID == Convert.ToInt32(ResponseEnum.NotExists))
                            return StatusCode(409, ErrorMessage.REGION_NOT_EXISTS);
                        return Ok(result);
                    }
                    return BadRequest(ErrorMessage.REGIONID_NOT_EXISTS);
                }
                return BadRequest(ErrorMessage.NO_PARAMETERS);
            }
            catch (FCCException fccEx)
            {
                _logger.LogError(fccEx, "Error in RegionController.Edit");
                return StatusCode(500, fccEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RegionController.Edit");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// GetById Region
        /// </summary>
        /// <returns>Details of Region</returns>
        [HttpGet("{Id}")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> GetById(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    _logger.LogInformation($"GetByIdRegion Start, Params: Region: {Id}");
                    var result = await _mediator.Send(new GetRegionByIdQuery(Id));
                    _logger.LogInformation($"GetByIdRegion END ");
                    if (result.RegionID == 0)
                        return BadRequest(ErrorMessage.REGIONID_NOT_EXISTS);
                    return Ok(result);
                }
                return BadRequest(ErrorMessage.INVALID_PARAMETERS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RegionController.GetById");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// GetAllRegion By ParentBusinessEntityID
        /// </summary>
        /// <returns>List OF RegionName</returns>
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> GetAll(int parentBusinessEntityId)
        {
            try
            {
                if (parentBusinessEntityId > 0)
                {
                    _logger.LogInformation($"GetAllRegionById Start, Params: Region: {parentBusinessEntityId}");
                    var result = await _mediator.Send(new GetAllRegionByParentIdQuery(parentBusinessEntityId));
                    _logger.LogInformation($"GetAllRegionById END ");
                    return Ok(result);
                }
                return BadRequest(ErrorMessage.INVALID_PARAMETERS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RegionController.GetById");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }
    
        
    }
}
