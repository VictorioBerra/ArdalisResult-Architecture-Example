namespace ArdalisResultArch.Application.Extensions;

using ArdalisResultArch.Application;
using ArdalisResultArch.Application.Validators;
using ArdalisResultArch.Domain.Models;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IValidator<EditBlog>, EditBlogValidator>();

        services.AddScoped<BlogService>();

        return services;
    }
}