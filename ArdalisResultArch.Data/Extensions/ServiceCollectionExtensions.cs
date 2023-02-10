namespace ArdalisResultArch.Data.Extensions;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationDataLayer(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryBlogContext>();

        return services;
    }
}