using Ardalis.Result;
using FluentValidation;
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
        private readonly IValidator<EditModel> editModelValidator;

        public EditModel(
            IValidator<EditModel> editModelValidator,
            BlogService blogService)
        {
            this.blogService = blogService;
            this.editModelValidator = editModelValidator;
        }

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

            var result = this.editModelValidator.Validate(this);

            if (result.Errors.Any())
            {
                // TODO move to extensions class
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return this.Page();
            }

            var updateResult = await this.blogService.EditBlogAsync(editBlogRequest);

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
                // IValidator for this page model would have returned friendly errors to the user
                // If the App Service returned Validation errors, its out of sync with the page model validator
                throw new NotImplementedException(
                    $"Unexpected result status from {nameof(this.blogService)}.{nameof(this.blogService.EditBlogAsync)}(...)");
            }
        }
    }
}
