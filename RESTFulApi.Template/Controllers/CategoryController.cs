using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulApi.Template.Models.Services;

namespace RESTFulApi.Template.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        /// <summary>
        /// لیست دسته بندی ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoryRepository.GetAll());
        }
        /// <summary>
        /// دریافت یک دسته بندی براساس شناسه
        /// </summary>
        /// <param name="id">شناسه دسته بندی</param>
        /// <returns></returns>
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
