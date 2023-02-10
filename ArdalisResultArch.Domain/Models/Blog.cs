namespace ArdalisResultArch.Domain.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<string> Tags { get; set; }

        public int CategoryId { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
