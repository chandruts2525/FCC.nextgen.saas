using FCC.Core;
using FCC.Core.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationStructure.Api.Controllers;
using OrganizationStructure.Service.Queries.Company;

namespace OrganizationManagementService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoreController : BaseController<CoreController>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CoreController> _logger;

        /// <summary>
        /// Company API controller constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// /// <param name="logger"></param>
        public CoreController(IMediator mediator, ILogger<CoreController> logger) : base(logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get Country Dropdown
        /// </summary>
        /// <returns>List of Country DropDown</returns>
        [HttpGet("GetCountry")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> GetCountry()
        {
            try
            {
                _logger.LogInformation($"GetCountry Start.");
                var result = await _mediator.Send(new GetCountryQuery());
                if (result != null)
                {
                    _logger.LogInformation($"GetCountry END.");
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CompanyController.GetCountry");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Get Measurement Type Dropdown
        /// </summary>
        /// <returns>List of Measurement Type DropDown</returns>
        [HttpGet("GetMeasurementType")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> GetMeasurementType()
        {
            try
            {
                _logger.LogInformation($"GetMeasurementType Start.");
                var result = await _mediator.Send(new GetMeasurementTypeQuery());
                if (result != null)
                {
                    _logger.LogInformation($"GetMeasurementType END.");
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CompanyController.GetMeasurementType");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Get Currency Dropdown
        /// </summary>
        /// <returns>List of Currency DropDown</returns>
        [HttpGet("GetCurrency")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> GetCurrency()
        {
            try
            {
                _logger.LogInformation($"GetCurrency Start.");
                var result = await _mediator.Send(new GetCurrencyQuery());
                if (result != null)
                {
                    _logger.LogInformation($"GetCurrency END.");
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CompanyController.GetCurrency");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Get Language Dropdown
        /// </summary>
        /// <returns>List of Language DropDown</returns>
        [HttpGet("GetLanguage")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> GetLanguage()
        {
            try
            {
                _logger.LogInformation($"GetLanguage Start.");
                var result = await _mediator.Send(new GetLanguageQuery());
                if (result != null)
                {
                    _logger.LogInformation($"GetLanguage END.");
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CompanyController.GetLanguage");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Get State Province Dropdown
        /// </summary>
        /// <returns>List of State Province DropDown</returns>
        [HttpGet("GetStateProvince")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> GetStateProvince(string countryCode)
        {
            try
            {
                _logger.LogInformation($"GetStateProvince Start.");
                if (!string.IsNullOrEmpty(countryCode))
                {
                    var result = await _mediator.Send(new GetStateProvinceByCountryCodeQuery(countryCode));
                    if (result != null)
                    {
                        _logger.LogInformation($"GetStateProvince END.");
                        return Ok(result);
                    }
                    return NoContent();
                }
                return BadRequest(ErrorMessage.NO_PARAMETERS);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CompanyController.GetStateProvince");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }

        /// <summary>
        /// Get Time Zones Dropdown
        /// </summary>
        /// <returns>List of Time Zones DropDown</returns>
        [HttpGet("GetTimeZones")]
        //[ValidateAntiForgeryToken]
        public ActionResult GetTimeZones()
        {
            try
            {
                _logger.LogInformation($"GetTimeZones Start.");
                var result = TimeZoneInfo.GetSystemTimeZones().Select(x => new { x.Id, x.DisplayName });
                if (result != null)
                {
                    _logger.LogInformation($"GetTimeZones END.");
                    return Ok(result);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CompanyController.GetTimeZones");
                return StatusCode(500, ErrorMessage.INTERNAL_SERVER_ERROR);
            }
        }
        
        /// <summary>
        /// Get all Date Format
        /// </summary>
        /// <returns>List of Date Format</returns>
        [HttpGet("GetDateFormat")]
        public IActionResult GetDateFormat()
        {
            Dictionary<string, string> result;
            result = new Dictionary<string, string>
            {
                { "1", "MM/DD/YY" },
                { "2", "DD/MM/YY" },
                { "3", "YY/MM/DD" },
                { "4", "Month D, Yr" },
                { "5", "M/D/YY" },
                { "6", "D/M/YY" },
                { "7", "YY/M/D" },
                { "8", "bM/bD/YY" },
                { "9", "bD/bM/YY" },
                { "A", "YY/bM/bD" },
                { "B", "MMDDYY" },
                { "C", "DDMMYY" },
                { "D", "YYMMDD" },
                { "E", "MonDDYY" },
                { "F", "DDMonYY" },
                { "G", "YYMonDD" },
                { "H", "day/YY" },
                { "I", "YY/day" },
                { "J", "D Month, Yr" },
                { "K", "Yr, Month D" },
                { "L", "Mon-DD-YYYY" },
                { "M", "DD-Mon-YYYY" },
                { "N", "YYYYY-Mon-DD" },
                { "O", "Mon DD, YYYY" },
                { "P", "DD Mon, YYYY" },
                { "Q", "YYYY, Mon DD" }
            };
            return Ok(result);
        }

        /// <summary>
        /// Get all Time Format
        /// </summary>
        /// <returns>List of Time Format</returns>
        [HttpGet("GetTimeFormat")]
        public IActionResult GetTimeFormat()
        {
            Dictionary<string, string> result;
            result = new Dictionary<string, string>
            {
                { "1", "HH:MM:SS" },
                { "2", "HH:MM:SS XM" },
                { "3", "HH:MM" },
                { "4", "HH:MM XM" }
            };
            return Ok(result);
        }
    }
}
