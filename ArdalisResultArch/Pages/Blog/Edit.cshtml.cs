using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using ArdalisResultArch.Application;
using ArdalisResultArch.Domain.Models;
using ArdalisResultArch.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArdalisResultArch.Pages.Blog
{
    public class EditModel : PageModel
    {
        private readonly BlogService blogService;

        public EditModel(BlogService blogService)
        {
            this.blogService = blogService;
        }

        [BindProperty]
        [System.ComponentModel.DataAnnotations.Required]
        public string MyProperty { get; set; }

        [BindProperty]
        public EditBlogViewModel EditBlogViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int blogId)
        {
            var blog = await this.blogService.GetBlogAsync(blogId);

            // Point of this .Map in Razor Pages?
            this.EditBlogViewModel = blog.Map(b => new EditBlogViewModel
            {
                Name = b.Name,
                Tags = b.Tags,
                CategoryId = b.CategoryId,
            });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int blogId)
        {
            var editBlogRequest = new EditBlog
            {
                Id = blogId,
                Name = this.EditBlogViewModel.Name,
                Tags = this.EditBlogViewModel.Tags,
            };

            var updateResult = await this.blogService.EditBlogAsync(editBlogRequest);

            // DEBUG
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            if (updateResult.ValidationErrors.Any())
            {
                foreach (var error in updateResult.ValidationErrors)
                {
                    this.ModelState.AddModelError(error.Identifier, error.ErrorMessage);
                }

                return this.Page();
            }

            if (updateResult.Status == ResultStatus.NotFound)
            {
                return this.NotFound();
            }
            else if (updateResult.Status == ResultStatus.Ok)
            {
                return this.RedirectToPage("/Index");
            }
            else
            {
                throw new NotImplementedException(
                    $"Unexpected result status from {nameof(this.blogService)}.{nameof(this.blogService.EditBlogAsync)}(...)");
            }
        }
    }
}
