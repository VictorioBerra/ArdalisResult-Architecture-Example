using ArdalisResultArch.Domain.Models;
using FluentValidation;

namespace ArdalisResultArch.Application.Validators
{
    public class EditBlogValidator : AbstractValidator<EditBlog>
    {
        public EditBlogValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
