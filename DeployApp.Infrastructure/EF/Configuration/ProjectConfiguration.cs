using DeployApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeployApp.Infrastructure.EF.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("project_001");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("id_001")
                .HasColumnOrder(0);
            builder.Property(p => p.Title)
                .HasColumnName("title_001")
                .HasColumnOrder(1)
                .IsRequired();
            builder.Property(p => p.Description)
                .HasColumnName("description_001")
                .HasColumnOrder(2)
                .IsRequired();
            builder.Property(p => p.IsActive)
                .HasColumnName("is_active_001")
                .HasColumnOrder(3);
            builder.Property(p => p.YtCode)
                .HasColumnName("yt_code_001")
                .HasColumnOrder(4)
                .IsRequired();
            builder.Property(p => p.RepositoryUrl)
                .HasColumnName("repository_url_001")
                .HasColumnOrder(5)
                .IsRequired();
            builder.HasIndex(p => p.Title)
                .IsUnique();
            builder.HasMany(p => p.Instances)
                .WithOne(i => i.Project)
                .HasForeignKey(i => i.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.ProjectVersions)
                .WithOne(v => v.Project)
                .HasForeignKey(v => v.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);            
        }
    }
}
