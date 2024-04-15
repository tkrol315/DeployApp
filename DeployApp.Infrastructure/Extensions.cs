using DeployApp.Application.Repositories;
using DeployApp.Infrastructure.EF.Contexts;
using DeployApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeployApp.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("Postgres")["ConnectionString"]; 
            services.AddDbContext<DeployAppDbContext>(context => context.UseNpgsql(connectionString));
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IInstanceRepository, InstanceRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            return services;
        }
    }
}
