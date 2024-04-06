using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfraDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITransientService, TransientService>();
        services.AddScoped<IScopedService, ScopedService>();
        services.AddSingleton<ISingletonService, SingletonService>();

        // Try to switch between Sendgrid and Mailgun
        //services.AddTransient<IEmailProvider, SendgridEmailProvider>();
        services.AddTransient<IEmailProvider, MailGunEmailProvider>();

        services.AddScoped<IProvinceRepository, ProvinceRepository>();

        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }

 public static IServiceCollection ConfigureInfraOptions(this IServiceCollection services, IConfiguration configuration)
{
    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0

    services.Configure<MailGunEmailProviderOptions>(options =>
    {
        configuration.GetSection("MailGunEmailProvider").Bind(options);
    });

    services.Configure<SendgridEmailProviderOptions>(options =>
    {
        configuration.GetSection("SendgridEmailProvider").Bind(options);
    });

    return services;
}

}