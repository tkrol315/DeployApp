using DeployApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace DeployApp.Infrastructure.EF.Configuration
{
    public class DeployLogConfiguration : IEntityTypeConfiguration<DeployLog>
    {
        public void Configure(EntityTypeBuilder<DeployLog> builder)
        {
            builder.ToTable("deploy_log_102");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id)
                .HasColumnName("id_102")
                .HasColumnOrder(0);
            builder.Property(d => d.DeployId)
                .HasColumnName("id_100_101_102")
                .HasColumnOrder(1);
            builder.Property(d => d.InstanceId)
               .HasColumnName("id_004_101_102")
               .HasColumnOrder(2);
            builder.Property(d => d.TimeStamp)
                .HasColumnName("timestamp_102")
                .HasColumnOrder(3);
            builder.Property(d => d.Status)
                .HasColumnName("status_102")
                .HasColumnOrder(4);
            builder.Property(d => d.Log)
                .HasColumnName("log_102")
                .HasColumnOrder(5);
            builder.HasOne(d => d.DeployInstance)
                .WithMany(d => d.DeployLogs)
                .HasForeignKey(d => new {d.DeployId,d.InstanceId})
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
