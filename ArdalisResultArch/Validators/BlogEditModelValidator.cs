namespace ArdalisResultArch.Validators;

using FluentValidation;
using ArdalisResultArch.Pages.Blog;

public class BlogEditModelValidator : AbstractValidator<EditModel>
{
    public BlogEditModelValidator()
    {
        RuleFor(x => x.EditBlogViewModel.Name)
            .NotEmpty()
            .MaximumLength(255);
    }
}