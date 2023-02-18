using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTFulApi.Template.Controllers.V2
{
    [ApiVersion("2")]
    [Route("api/V{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : V1.ValuesController
    {
        [HttpGet]
        public override IEnumerable<string> Get()
        {
            return new[] { "V2 Item1", "V2 Item2", "V2 Item3" };
        }


        [HttpGet("{name}")]
        public IActionResult Test(string name)
        {
            return Ok(name);
        }
    }
}
