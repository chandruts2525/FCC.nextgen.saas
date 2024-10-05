using FCC.Core;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FCC.Core.Constants;
using OrganizationStructure.Service.ViewModel.Yard;
using OrganizationManagementService.Service.Commands.Yard;
using OrganizationManagementService.Service.Queries.Yard;
using OrganizationStructure.Service.Commands.Yard;

namespace OrganizationStructure.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class YardController : BaseController<YardController>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<YardController> _logger;

        /// <summary>
		/// Yard API controller constructor
		/// </summary>
		/// <param name="mediator"></param>
		/// /// <param name="logger"></param>
		public YardController(IMediator mediator, ILogger<YardController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// This method enables the caller to Get General Ledger Dropdown
        /// </summary>
        /// <returns>List of General Ledger DropDown</returns>
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Get(int ParentBusinessEntityId)
        {
            try
            {
                _logger.LogInformation($"GetYard Start.");
                var result = await _mediator.Send(new GetYardQuery(ParentBusinessEntityId));
                if (result != null)
                {
                    _logger.LogInformation($"GetYard END.");
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in YardController.GetYard");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
		/// This method enables the caller to create a <see cref="Yard"/> 
		/// </summary>
		/// <param name="CreateYardVM"><see cref="createYardVM"/></param>
		/// <returns>BusinessEntity Id</returns>
		[HttpPost]
        public async Task<IActionResult> Create(CreateYardVM? createYardVM)
        {
            try
            {
                if (!string.IsNullOrEmpty(createYardVM?.BusinessEntityName))
                {
                    _logger.LogInformation($"CreateYard Start, Params: Yard: {createYardVM.BusinessEntityName}");
                    var result = await _mediator.Send(new CreateYardCommand(createYardVM));
                    if (result != null)
                    {
                        _logger.LogInformation($"CreateYard Complete. Params - Yard: {result.BusinessEntityName}");
                        if (result.BusinessEntityId == Convert.ToInt32(ResponseEnum.NotExists))
                            return StatusCode(203, new
                            {
                                name = result.BusinessEntityName != ErrorMessage.NAME_ALREADY_EXIST_ERROR ? "" : result.BusinessEntityName,
                                code = result.BusinessEntityCode != ErrorMessage.CODE_ALREADY_EXIST_ERROR ? "" : result.BusinessEntityCode
                            });
                        return Ok(result);
                    }
                    else
                        return StatusCode(500, ErrorMessage.UNKNOWN_ERROR);
                }
                return BadRequest(ErrorMessage.NO_PARAMETERS);
            }
            catch (FCCException fccEx)
            {
                _logger.LogError(fccEx, "Error in YardController.CreateYard");
                return StatusCode(500, fccEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in YardController.CreateYard");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
		/// Update Yard
		/// </summary>
		/// <param name="updateYardVM"></param><see cref="Yard"/></param>
		/// <returns>Yard Id</returns>
		[HttpPut]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateYardVM? updateYardVM)
        {
            try
            {
                if (updateYardVM?.BusinessEntityId > 0 && !string.IsNullOrEmpty(updateYardVM.BusinessEntityName))
                {
                    _logger.LogInformation($"EditYard Start, Params: Yard: {updateYardVM.BusinessEntityId}");
                    var result = await _mediator.Send(new UpdateYardCommand(updateYardVM));
                    if (result != null)
                    {
                        _logger.LogInformation($"EditYard Complete. Params - Yard: {result.BusinessEntityName}");
                        if (result.BusinessEntityId == Convert.ToInt32(ResponseEnum.NotExists))
                            return StatusCode(203, new
                            {
                                name = result.BusinessEntityName != ErrorMessage.NAME_ALREADY_EXIST_ERROR ? "" : result.BusinessEntityName,
                                code = result.BusinessEntityCode != ErrorMessage.CODE_ALREADY_EXIST_ERROR ? "" : result.BusinessEntityCode
                            });
                        return Ok(result);
                    }
                    else
                        return StatusCode(500, ErrorMessage.UNKNOWN_ERROR);
                }
                return BadRequest(ErrorMessage.NO_PARAMETERS);
            }
            catch (FCCException fccEx)
            {
                _logger.LogError(fccEx, "Error in YardController.EditYard");
                return StatusCode(500, fccEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in YardController.EditYard");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Get yard details by business entity id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GetYardByIdResponseVM model</returns>
        [HttpGet("{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                if (id > 0)
                {
                    GetYardByIdResponseVM getYardByIdResponseVM = new GetYardByIdResponseVM()
                    {
                        BusinessEntityId = id,
                    };
                    _logger.LogInformation($"Getyard Start, Params: Yard: {getYardByIdResponseVM.BusinessEntityId}");
                    var result = await _mediator.Send(new GetYardByIdQuery(getYardByIdResponseVM));
                    if (result != null)
                    {
                        _logger.LogInformation($"Getyard Complete. Params - Yard Count: {result}");
                        return Ok(result);
                    }
                    return StatusCode(500, ErrorMessage.DATABASE_ERROR);
                }
                return BadRequest(ErrorMessage.NO_PARAMETERS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in YardController.Getyard");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
		/// Get yard details by business entity id 
		/// </summary>
		/// <param name="id"></param>
		/// <returns>GetYardByIdResponseVM model</returns>
		[HttpPut("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int BusinessEntityId)
        {
            try
            {
                if (BusinessEntityId > 0)
                {
                    _logger.LogInformation($"Deleteyard Start.");
                    var result = await _mediator.Send(new DeleteYardCommand(BusinessEntityId));
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in YardController.Deleteyard");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
		/// This method enables the caller to Get General Ledger Dropdown
		/// </summary>
		/// <returns>List of General Ledger DropDown</returns>
		[HttpGet("GeneralLedger")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> GetGeneralLedger()
        {
            try
            {
                _logger.LogInformation($"GetGeneralLedger Start.");
                var result = await _mediator.Send(new GetGeneralLedgerQuery());
                if (result != null)
                {
                    _logger.LogInformation($"GetGeneralLedger END.");
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in YardController.GetGeneralLedger");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
		/// This method enables the caller to Get GetTermsFooter Dropdown
		/// </summary>
		/// <returns>List of General GetTermsFooter DropDown</returns>
		[HttpGet("TermsFooter")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> GetTermsFooter()
        {
            try
            {
                _logger.LogInformation($"GetTermsFooter Start.");
                var result = await _mediator.Send(new GetTermsFooterQuery());
                if (result != null)
                {
                    _logger.LogInformation($"GetTermsFooter END.");
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in YardController.GetTermsFooter");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }
    }
}
