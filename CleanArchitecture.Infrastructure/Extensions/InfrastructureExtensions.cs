using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.Messaging.Publishers;
using CleanArchitecture.Infrastructure.Persistence;
using MassTransit;
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

            services.AddMassTransit(x =>
            {
                // Đăng ký tất cả consumer trong assembly hiện tại
                x.AddConsumers(typeof(InfrastructureExtensions).Assembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["MessageBroker:Host"], "/", h =>
                    {
                        h.Username(configuration["MessageBroker:Username"]!);
                        h.Password(configuration["MessageBroker:Password"]!);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped(typeof(IPublisher<>), typeof(RabbitMqPublisher<>));
            // Đăng ký repository, unit of work, các service khác ở Infrastructure
            //services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
