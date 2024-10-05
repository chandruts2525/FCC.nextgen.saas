using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FCC.SPA.Abstractions;
using FCC.SPA.Filters;
using FCC.SPA.Models;

namespace FCC.SPA.Controllers
{
	[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WebAppController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IApiService _apiService;
        public WebAppController(IApiService apiService, IConfiguration configuration)
        {

            _apiService = apiService;
            _configuration = configuration;
        }



        [HttpPost]
        [Produces("application/json")]
        [IgnoreAntiforgeryToken]
        [TypeFilter(typeof(AuditFilterAttribute), Arguments = new object[] { Constants.Others })]
        public async Task<IActionResult> ExecuteRequestAsync(InputParameters param)
        {
            try
            {
                var result = await _apiService.ExecuteRequestAsync(param);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(result.ResponseValue);
                }
                else
                {
                    return StatusCode((int)result.StatusCode, result.ResponseValue); 
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }


        }

        [HttpPost]
        [Produces("application/json")]
        [Route("upload/attachments")]
        [TypeFilter(typeof(AuditFilterAttribute), Arguments = new object[] { Constants.Others })]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ExecuteAttachmentRequestAsync([FromForm] InputParameters param)
        {
            try
            {
                var result = await _apiService.ExecuteRequestAsync(param);

                return Ok(result.ResponseValue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }

   
}
