namespace ArdalisResultArch.Domain.Models
{
    public class EditBlog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Tags { get; set; }

        public int CategoryId { get; set; }
    }
}
