using DeployApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DeployApp.Infrastructure.EF.Configuration
{
    public class DeployInstanceConfiguration : IEntityTypeConfiguration<DeployInstance>
    {
        public void Configure(EntityTypeBuilder<DeployInstance> builder)
        {
            builder.ToTable("deploy_instance_101");
            builder.HasKey(di => new {di.DeployId, di.InstanceId});
            builder.Property(di => di.DeployId)
                .HasColumnName("id_100_101")
                .HasColumnOrder(0);
            builder.Property(di => di.InstanceId)
                .HasColumnName("id_004_101")
                .HasColumnOrder(1);
            builder.Property(di => di.Status)
                .HasColumnName("status_101")
                .HasColumnOrder(2);
            builder.HasOne(di => di.Instance)
                .WithMany(i => i.DeployInstances)
                .HasForeignKey(di => di.InstanceId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(di => di.Deploy)
                .WithMany(d => d.DeployInstances)
                .HasForeignKey(di => di.DeployId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
