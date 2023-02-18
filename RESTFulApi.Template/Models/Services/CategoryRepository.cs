using RESTFulApi.Template.Models.Context;
using RESTFulApi.Template.Models.Entities;

namespace RESTFulApi.Template.Models.Services
{
    public class CategoryRepository
    {
        private readonly DatabaseContext _context;
        public CategoryRepository(DatabaseContext context)
        {
            _context = context; 
        }

        public List<CategoryDto> GetAll()
        {
            return _context.Categories.Select(p => new CategoryDto
            {
                Id = p.Id,
                Name = p.Name,  
            }).ToList();
        }

        public CategoryDto GetBy(int id) 
        {
            var category = _context.Categories.Find(id);
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public int AddCategory(string name)
        {
            _context.Categories.Add(new Category { Name = name });
            return _context.SaveChanges();
        }

        public int EditCategory(CategoryDto categoryDto)
        {
            var category = _context.Categories.SingleOrDefault(p => p.Id == categoryDto.Id);
            category.Name = categoryDto.Name;
            return _context.SaveChanges();
        }

        public int DeleteCategory(int id) 
        {
            _context.Categories.Remove(new Category { Id = id });
            return _context.SaveChanges();
        }


    }
}
