using Bookify.Application.Abstraction.Clock;
using Bookify.Application.Abstraction.Email;
using Bookify.Infrastructure.Clock;
using Bookify.Infrastructure.Configurations;
using Bookify.Infrastructure.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookify.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection service , 
            IConfiguration  configuration)
        {
            service.AddTransient<IDateTimeProvider, DateTimeProvider>();
			service.AddTransient<IEmailService, EmailServices>();

            var connectionString = configuration.GetConnectionString("Database") ??
                throw new ArgumentNullException(nameof(configuration));

            service.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();     
                });

			return service;
        }
    }
}