using Microsoft.AspNetCore.Mvc;

namespace RESTFulApi.Template.Controllers
{
    [Route("api/ToDo/{ToDoId}/categories/{CategoryId}")]
    [ApiController]
    public class ToDoCategoryController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(int ToDoId, int CategoryId)
        {
            return Ok();
        }
    }
}
