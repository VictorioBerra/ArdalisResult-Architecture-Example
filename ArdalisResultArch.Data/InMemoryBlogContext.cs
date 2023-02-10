using ArdalisResultArch.Domain.Entities;

namespace ArdalisResultArch.Data
{
    /// <summary>
    /// Pretend this is a DbContext
    /// </summary>
    public class InMemoryBlogContext
    {
        public List<Blog> Blogs { get; set; } = new List<Blog>();

        public InMemoryBlogContext()
        {
            this.Blogs.Add(new Blog
            {
                Id = 1,
                Name = "Hello World",
                Tags = new List<string> { "Hello", "World" },
                CategoryId = 1,
                LastUpdated = DateTime.Now
            });
        }

        public void SaveChanges()
        {
            // Pretend this is saving changes to a database
        }
    }
}