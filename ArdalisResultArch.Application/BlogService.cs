using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using ArdalisResultArch.Data;
using ArdalisResultArch.Domain.Models;
using FluentValidation;

namespace ArdalisResultArch.Application
{
    public class BlogService
    {
        private readonly IValidator<EditBlog> editBlogValidator;
        private readonly InMemoryBlogContext context;

        public BlogService(
            IValidator<EditBlog> editBlogValidator,
            InMemoryBlogContext context)
        {
            this.editBlogValidator = editBlogValidator;
            this.context = context;
        }

        public Task<Result<Blog>> GetBlogAsync(int blogId)
        {
            if (blogId == default)
            {
                // Simulate async
                return Task.FromResult(Result<Blog>.NotFound());
            }
            
            var blog = context.Blogs.SingleOrDefault(x => x.Id == blogId);
            if (blog == null)
            {
                return Task.FromResult(Result<Blog>.NotFound());
            }

            // Map blog entity to blog domain dto
            var returnResult = new Blog
            {
                Id = blog.Id,
                Name = blog.Name,
                Tags = blog.Tags,
                CategoryId = blog.CategoryId,
                LastUpdated = blog.LastUpdated,
            };

            return Task.FromResult(Result<Blog>.Success(returnResult));
        }

        public async Task<Result<Blog>> EditBlogAsync(EditBlog editBlogRequest)
        {
            if (editBlogRequest.Id == default)
            {
                return Result<Blog>.NotFound();
            }

            var validation = await editBlogValidator.ValidateAsync(editBlogRequest);
            if (!validation.IsValid)
            {
                return Result<Blog>.Invalid(validation.AsErrors());
            }

            var itemToUpdate = context.Blogs.SingleOrDefault(x => x.Id == editBlogRequest.Id);
            if (itemToUpdate == null)
            {
                return Result<Blog>.NotFound();
            }

            // Map edit dto to blog entity
            itemToUpdate.Name = editBlogRequest.Name;
            itemToUpdate.Tags = editBlogRequest.Tags;
            itemToUpdate.CategoryId = editBlogRequest.CategoryId;
            itemToUpdate.LastUpdated = DateTime.Now;
            context.SaveChanges();

            // Map blog entity to blog domain dto
            var returnResult = new Blog
            {
                Id = itemToUpdate.Id,
                Name = itemToUpdate.Name,
                Tags = itemToUpdate.Tags,
                CategoryId = itemToUpdate.CategoryId,
                LastUpdated = itemToUpdate.LastUpdated,
            };

            return Result<Blog>.Success(returnResult);
        }
    }
}