using FCC.Core;
using FCC.Core.Constants;
using FCC.Core.ViewModel.GridFilter;
using WorkManagement.Domain.ViewModel.UnitOfMeasure;
using WorkManagement.Service.Commands.UnitOfMeasure;
using WorkManagement.Service.Queries.UnitOfMeasure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WorkManagement.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UnitOfMeasureController : BaseController<UnitOfMeasureController>
	{
		private readonly IMediator _mediator;
		private readonly ILogger<UnitOfMeasureController> _logger;

		public UnitOfMeasureController(IMediator mediator, ILogger<UnitOfMeasureController> logger) : base(logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		/// <summary>
		/// Get roles list based on search
		/// </summary>
		/// <param name="gridFilter"></param>
		/// <returns>GetAllUnitOfMeasuresListFilterResponseVM models</returns>
		[HttpPost("Search")]
		public async Task<ActionResult> Search(GridFilter gridFilter)
		{
			try
			{
				_logger.LogInformation($"GetAllUnitofMeasureList Start");
				var result = await _mediator.Send(new GetAllUnitOfMeasuresQuery(gridFilter));
				if (result != null)
				{
					_logger.LogInformation($"GetAllUnitofMeasureList End");
					return Ok(result);
				}
				return StatusCode(500, ErrorMessage.DATABASE_ERROR);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UnitOfMeasureController.Get AllUnitOfMeasure List");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}
		/// <summary>
		/// This method enables the caller to create a <see cref="UnitOfMeasures"/>
		/// </summary>
		/// <param name="UnitOfMeasures"><see cref="UnitOfMeasures"/></param>
		/// <returns>Message</returns>
		[HttpPost]
        public async Task<ActionResult> Create(UnitOfMeasureResponseVM unitOfMeasureVM)
        {
            try
            {
                if (unitOfMeasureVM.UnitMeasureCode != null)
                {
                    _logger.LogInformation("Create Unit Of Measure Start, Params: UnitMeasureCode: {unitOfMeasureVM.UnitMeasureCode}, CreatedBy: {unitOfMeasureVM.SavedBy}", unitOfMeasureVM.UnitMeasureCode, unitOfMeasureVM.SavedBy);
                    var result = await _mediator.Send(new CreateUnitOfMeasureCommand(unitOfMeasureVM));
                    if (result != null)
                    {
                        string resUMC = result.UnitMeasureCode;
                        _logger.LogInformation($"Create Unit Of Measure Complete. Params - UnitMeasureCode:",resUMC);
                        
                        return Ok(result);
                    }
                    return StatusCode(500, ErrorMessage.DATABASE_ERROR);
                }
                return BadRequest(ErrorMessage.NO_PARAMETERS);
            }
            catch (FCCException fccEx)
            {
                _logger.LogError(fccEx, "Error in UnitOfMeasureController.CreateUnitOfMeasure");
                return StatusCode(203, fccEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UnitOfMeasureController.Create UnitOfMeasure");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }
		/// <summary>
		/// This method enables the caller to Update a <see cref="UnitOfMeasures"/>
		/// </summary>
		/// <param name="UnitOfMeasures"><see cref="UnitOfMeasures"/></param>
		/// <returns>Message</returns>
		[HttpPut]
		public async Task<ActionResult> Edit(UnitOfMeasureResponseVM unitOfMeasureVM)
		{
			try
			{
				if (unitOfMeasureVM.UnitMeasureCode != null)
				{
					_logger.LogInformation($"Update Unit Of Measure Start, Params: UnitMeasureCode: {unitOfMeasureVM.UnitMeasureCode}, ModifiedBy: {unitOfMeasureVM.SavedBy}");
					var result = await _mediator.Send(new UpdateUnitOfMeasureCommand(unitOfMeasureVM));
					if (result != null)
					{
						_logger.LogInformation($"Update Unit Of Measure Complete.UnitMeasureCode: {result.UnitMeasureCode}");

						return Ok(result);
					}
					return StatusCode(500, ErrorMessage.DATABASE_ERROR);
				}
				return BadRequest(ErrorMessage.NO_PARAMETERS);
			}
			catch (FCCException fccEx)
			{
				_logger.LogError(fccEx, "Error in UnitOfMeasureController.UpdateUnitOfMeasure");
				return StatusCode(203, fccEx.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UnitOfMeasureController.Update UnitOfMeasure");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}
		/// <summary>
		/// This method enables the caller to GetAll <see cref="UnitOfMeasures"/>
		/// </summary>
		/// <param name="UnitOfMeasures"><see cref="UnitOfMeasures"/></param>
		/// <returns>List Of UnitOfMeasure Model</returns>
		[HttpGet("GetMeasureType")]
		public async Task<ActionResult> GetAllUnitofMeasureTypes()
		{
			try
			{
				_logger.LogInformation($"GetAllUnitofMeasureTypes Start");
				var result = await _mediator.Send(new GetAllUnitofMeasureTypesQuery());
				if (result != null)
				{
					return Ok(result);
				}
				return StatusCode(500, ErrorMessage.DATABASE_ERROR);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UnitOfMeasureController.Get UnitOfMeasureTypes");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}
		/// <summary>
		///  This method enables the caller to UnitOfMeasureDeactivate <see cref="UnitOfMeasures"/>
		/// </summary>
		/// <param name="UnitMeasureCode"></param>
		/// <param name="IsActive"></param>
		/// <returns></returns>
		[HttpPut("Status")]
		public async Task<ActionResult> Status(int UnitMeasureId, bool Status)
		{
			try
			{
				if (UnitMeasureId > 0)
				{
					UnitOfMeasureResponseVM unitOfMeasure = new UnitOfMeasureResponseVM();
					unitOfMeasure.UnitMeasureId = UnitMeasureId;
					unitOfMeasure.IsActive = Status;
					_logger.LogInformation($"ActivateDeactivateUOM Start.");
					var result = await _mediator.Send(new ActivateDeactivateUnitOfMeasureCommand(unitOfMeasure));
					if (result.UnitMeasureCode != null)
						return Ok("Status Updated Successfully");
					else
						return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
				}
				return BadRequest(ErrorMessage.INVALID_PARAMETERS);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error in UnitOfMeasureController.ActivateDeactivateUOM");
				return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
			}
		}
	}
}
