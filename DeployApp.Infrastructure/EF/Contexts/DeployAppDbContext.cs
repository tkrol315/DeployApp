using DeployApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeployApp.Infrastructure.EF.Contexts
{
    public class DeployAppDbContext(DbContextOptions<DeployAppDbContext> options) : DbContext(options)
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Instance> Instances { get; set; }
        public DbSet<ProjectVersion> ProjectVersions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Domain.Entities.Type> Types { get; set; }
        public DbSet<InstanceTag> InstanceTags { get; set; }
        public DbSet<InstanceGroup> InstanceGroups { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

    }
}
