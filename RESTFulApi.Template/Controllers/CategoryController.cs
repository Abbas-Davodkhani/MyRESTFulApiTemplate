using Microsoft.AspNetCore.Mvc;
using RESTFulApi.Template.Models.Services;

namespace RESTFulApi.Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoryRepository.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            return Ok(_categoryRepository.GetBy(id));
        }
        [HttpPost]
        public IActionResult Post(string name)
        {
            var result = _categoryRepository.AddCategory(name);
            return Created(Url.Action(nameof(Get), "Category", new { id = result }, Request.Scheme), true);
        }
        [HttpPut]
        public IActionResult Put(CategoryDto categoryDto)
        {
            return Ok(_categoryRepository.EditCategory(categoryDto));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok(_categoryRepository.DeleteCategory(id));
        }
    }
}
