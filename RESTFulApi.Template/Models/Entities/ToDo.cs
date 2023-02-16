namespace RESTFulApi.Template.Models.Entities
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertDate { get; set; }
        public bool IsRemove { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
