namespace RESTFulApi.Template.Models
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertDate { get; set; }
        public List<HATEOAS> HATEOAs { get; set; }
    }
}
