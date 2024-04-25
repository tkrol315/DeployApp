using DeployApp.Application.Abstractions;
using DeployApp.Application.Repositories;
using DeployApp.Infrastructure.Abstractions;
using DeployApp.Infrastructure.EF.Contexts;
using DeployApp.Infrastructure.Middleware;
using DeployApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DeployApp.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File("log.txt").CreateLogger();
            var connectionString = configuration.GetSection("Postgres")["ConnectionString"]; 
            services.AddDbContext<DeployAppDbContext>(context => context.UseNpgsql(connectionString));
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IInstanceRepository, InstanceRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IProjectVersionConverter, ProjectVersionConverter>();
            services.AddScoped<ITransactionHandler, TransactionHandler>();
            services.AddScoped<ErrorHandlingMiddleware>();
            return services;
        }
        public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            return app;
        }
    }
}
