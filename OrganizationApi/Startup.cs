using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OrganizationApi.Consumers;
using OrganizationApi.Data;
using System;
using UserApi.Contract.Configuration;

namespace OrganizationApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Organization API", Version = "v1" });
            });
            services.AddMvc();
            services.AddControllers();
            var connectionString = Environment.GetEnvironmentVariable("PSQL_CONNECTION_STRING")
                ?? "User ID=postgres;Password=Mehanik21;Host=localhost;Port=5433;Database=user_api_new;Pooling=true;";
            services.AddDbContext<ApplicationDbContext>(x
                => x.UseNpgsql(connectionString));
            services.AddScoped<IApplicationDbContext>(x => x.GetRequiredService<ApplicationDbContext>());

            var rabbitHost = Environment.GetEnvironmentVariable("RABBIT_HOST") ?? "localhost";
            var rabbitHostVirtualHost = Environment.GetEnvironmentVariable("RABBIT_HOST_VIRTUAL_HOST") ?? "/";
            var rabbitHostUserName = Environment.GetEnvironmentVariable("RABBIT_USERNAME") ?? "guest";
            var rabbitHostPassword = Environment.GetEnvironmentVariable("RABBIT_PASSWORD") ?? "guest";

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(rabbitHost, rabbitHostVirtualHost, h => {
                    h.Username(rabbitHostUserName);
                    h.Password(rabbitHostPassword );
                });
            }));
            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(AppMappingProfile));

            services.AddMassTransit(c =>
            {
                c.AddConsumer<UserMessageConsumer>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment _)
        {
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Organization API V1");
                c.RoutePrefix = String.Empty;
            });
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
