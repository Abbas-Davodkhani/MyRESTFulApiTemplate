using Microsoft.EntityFrameworkCore;
using RESTFulApi.Template.Models.Context;
using RESTFulApi.Template.Models.Entities;

namespace RESTFulApi.Template.Models.Services
{
    public class ToDoRepository
    {
        private readonly DatabaseContext _context;
        public ToDoRepository(DatabaseContext context)
        {
            _context = context; 
        }

        public List<ToDoDto> GetAll()
        {
            return _context.ToDos.Select(p => new ToDoDto
            {
                Id = p.Id,
                Categories= p.Categories,
                InsertDate= p.InsertDate,
                Text = p.Text, 
            }).ToList();
        }


        public ToDoDto GetById(int id)
        {
            var resutl = _context.ToDos.FirstOrDefault(p => p.Id == id);
            return new ToDoDto
            {
                Id = resutl.Id,
                Categories = resutl.Categories,
                InsertDate = resutl.InsertDate,
                Text = resutl.Text,
            };
        }

        public AddToDoDto Add(AddToDoDto addToDoDto)
        {
            ToDo newToDo = new ToDo
            {
                Id = addToDoDto.toDoDto.Id ,
                Text = addToDoDto.toDoDto.Text , 
                InsertDate = DateTime.Now , 
                IsRemove = false , 
                
            };

            foreach (var item in addToDoDto.Categories)
            {
                var category = _context.Categories.FirstOrDefault(x => x.Id == item);
                newToDo.Categories.Add(category);
            }
            _context.Add(newToDo);
            _context.SaveChanges();

            return new AddToDoDto
            {
                toDoDto = new ToDoDto
                {
                    Id = newToDo.Id , 
                    Text = newToDo.Text , 
                    InsertDate = newToDo.InsertDate , 
                    IsRemove = newToDo.IsRemove 
                },
                Categories = addToDoDto.Categories
            };

        }

        public void Delete(int id) 
        {
            // _context.ToDos.Remove(new ToDo { Id = id });
            var todo = _context.ToDos.FirstOrDefault(x => x.Id == id);
            todo.IsRemove = true;
            _context.SaveChanges();
        }

        public bool Edit(EditToDoDto editToDoDto)
        {
            var todo = _context.ToDos.FirstOrDefault(x => x.Id == editToDoDto.Id);
            todo.Text = editToDoDto.Text;
            _context.SaveChanges();
            return true;
        }
    }

    public class ToDoDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsRemove { get; set; }
        public ICollection<Category> Categories { get; set; }
    }

    public class AddToDoDto
    {
        public ToDoDto toDoDto { get; set; }
        public List<int> Categories { get; set; } = new List<int>();
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ToDoDto> ToDoDtos { get; set; }
    }

    public class EditToDoDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<int> Categories { get; set; }
    }
}
