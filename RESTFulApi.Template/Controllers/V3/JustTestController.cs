using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTFulApi.Template.Controllers.V3
{
    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class JustTestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Test Successfuly");
        }
    }
}
