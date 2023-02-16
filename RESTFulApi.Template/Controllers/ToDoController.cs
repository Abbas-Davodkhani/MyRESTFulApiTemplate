using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTFulApi.Template.Models;
using RESTFulApi.Template.Models.Services;

namespace RESTFulApi.Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoRepository _toDoRepository;
        public ToDoController(ToDoRepository toDoRepository)
        {
            _toDoRepository= toDoRepository;
        }
        [HttpGet]
        public List<ToDoItemDto> Get()
        {
            return _toDoRepository.GetAll().Select(p => new ToDoItemDto
            {
                Id = p.Id,
                InsertDate = p.InsertDate,
                Text = p.Text,
                HATEOAs = new List<HATEOAS>
                {
                    new HATEOAS{Url = Url.Action(nameof(Get) , "ToDo"  , Request.Scheme) , Method = "Get" , Rel = "Get"},
                    new HATEOAS{Url = Url.Action(nameof(Get) , "ToDo"  , new { id = p.Id } , Request.Scheme) , Method = "Get" , Rel = "Get"},
                    new HATEOAS{Url = Url.Action(nameof(Create) , "ToDo"  , Request.Scheme) , Method = "Post" , Rel ="POST" },
                    new HATEOAS{Url = Url.Action(nameof(Delete) , "ToDo" , Request.Scheme) , Method = "DELETE" , Rel ="DELETE"},
                    new HATEOAS{Url = Url.Action(nameof(Edit), "ToDo", Request.Scheme), Method = "PUT", Rel = "PUT"},
                }
            }).ToList();
            
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _toDoRepository.GetById(id);
            return Ok(new ToDoItemDto { Id = result.Id, InsertDate = result.InsertDate, Text = result.Text });
        }
        [HttpPost]
        public IActionResult Create([FromBody]ItemDto addItemDto)
        {
            var result = _toDoRepository.Add(new AddToDoDto
            {
                toDoDto = new ToDoDto
                {
                    Text = addItemDto.Text
                }
            });

            string url = Url.Action(nameof(Get), "ToDo", new { id = result.toDoDto.Id } , Request.Scheme);

            return Created(url , true);
        }
        [HttpPut]
        public IActionResult Edit([FromBody] EditToDoDto editToDoDto)
        {
            var result = _toDoRepository.Edit(editToDoDto);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _toDoRepository.Delete(id);
            return Ok();
        }
        
    }
}
