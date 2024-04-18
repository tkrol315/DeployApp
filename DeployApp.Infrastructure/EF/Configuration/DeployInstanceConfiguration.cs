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
            builder.Property(di => di.DeployId)
                .HasColumnName("id_100_101")
                .HasColumnOrder(0);
            builder.Property(di => di.InstanceId)
                .HasColumnName("id_004_101")
                .HasColumnOrder(1);
            builder.Property(di => di.Status)
                .HasColumnName("status_101")
                .HasColumnOrder(2);
        }
    }
}
