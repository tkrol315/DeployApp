using DeployApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeployApp.Infrastructure.EF.Configuration
{
    public class ProjectVersionConfiguration : IEntityTypeConfiguration<ProjectVersion>
    {
        public void Configure(EntityTypeBuilder<ProjectVersion> builder)
        {
            builder.ToTable("project_version_002");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
                .HasColumnName("id_002")
                .HasColumnOrder(0);
            builder.Property(v => v.ProjectId)
                .HasColumnName("id_001_002")
                .HasColumnOrder(1);
            builder.Property(v => v.Major)
                .HasColumnName("major_002")
                .HasColumnOrder(2);
            builder.Property(propertyExpression: v => v.Minor)
                .HasColumnName("minor_002")
                .HasColumnOrder(3);
            builder.Property(propertyExpression: v => v.Patch)
                .HasColumnName("patch_002")
                .HasColumnOrder(4);
            builder.Property(propertyExpression: v => v.Description)
                .HasColumnName("description_002")
                .HasColumnType("varchar(250)")
                .HasColumnOrder(5)
                .IsRequired();
            builder.HasOne(v => v.Project)
                .WithMany(p => p.ProjectVersions)
                .HasForeignKey(v => v.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
