using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReadingXML.Application.Interfaces;
using ReadingXML.Application.Services;
using ReadingXML.Data.Context;
using ReadingXML.Data.Repositories;
using ReadingXML.Domain.Interfaces;

namespace ReadingXML.IoC
{
    public static class DependencyInjectinAPI
    {
        public static IServiceCollection AddInfrastructureAPI
            (
                this IServiceCollection services,
                IConfiguration configuration
            )
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LocalDb"), b =>
                b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            // Injeção de Dependências Repositories
            services.AddScoped<IXMLExtractRepository, XMLExtractRepository>();


            // Injeção de Dependências Services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDeserializeXMLService, DeserializeXMLService>();
            services.AddScoped<IXMLExtractService, XMLExtractService>();

            return services;
        }
    }
}
