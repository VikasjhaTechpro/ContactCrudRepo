using ContactApp.Application.Interfaces;
using ContactApp.Domain.Entities;
using ContactApp.Infrastructure.Data;
using ContactApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Infrastructure.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Use SQLite for simplicity in sample project. Connection string from configuration.
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ?? "Data Source=contacts.db"));

            services.AddScoped<IRepository<Contact>, ContactRepository>();

            return services;
        }
    }
}
