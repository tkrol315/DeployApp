using DeployApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeployApp.Infrastructure.EF.Configuration
{
    public class DeployConfiguration : IEntityTypeConfiguration<Deploy>
    {
        public void Configure(EntityTypeBuilder<Deploy> builder)
        {
            builder.ToTable("deploy_100");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id)
                .HasColumnName("id_100")
                .HasColumnOrder(0);
            builder.Property(d => d.ProjectId)
                .HasColumnName("id_001_100")
                .HasColumnOrder(1);
            builder.Property(d => d.ProjectVersionId)
                .HasColumnName("id_002_100")
                .HasColumnOrder(2);
            builder.Property(d => d.Start)
                .HasColumnName("start_100")
                .HasColumnOrder(3);
            builder.Property(d => d.End)
                .HasColumnName("end_100")
                .HasColumnOrder(4);
            builder.Property(d => d.Description)
                .HasColumnName("description_100")
                .HasColumnType("varchar(250)")
                .HasColumnOrder(5);
            builder.Property(d => d.IsActive)
                .HasColumnName("is_active_100")
                .HasColumnOrder(6);
            builder.HasOne(d => d.ProjectVersion)
                .WithMany(pv => pv.Deploys)
                .HasForeignKey(d => d.ProjectVersionId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(d => d.Project)
                .WithMany(p => p.Deploys)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
