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
                .IsRequired()
                .HasColumnName("title_001")
             .HasColumnOrder(1);
            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnName("description_001")
             .HasColumnOrder(2);
            builder.Property(p => p.IsActive)
                .IsRequired()
                .HasColumnName("is_active_001")
                 .HasColumnOrder(3);
            builder.Property(p => p.YtCode)
                .IsRequired()
                .HasColumnName("yt_code_001")
                 .HasColumnOrder(4);
            builder.Property(p => p.RepositoryUrl)
                .IsRequired()
                .HasColumnName("repository_url_001")
                 .HasColumnOrder(5);
            builder.HasMany(p => p.Instances)
                .WithOne(i => i.Project)
                .HasForeignKey(i => i.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(p => p.ProjectVersions)
                .WithOne(v => v.Project)
                .HasForeignKey(v => v.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
