using CleanArchitecture.Application.Authentication;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            // Register services
            services.AddScoped<JwtTokenGenerator>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            // Đăng ký repository, unit of work, các service khác ở Infrastructure
            //services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
