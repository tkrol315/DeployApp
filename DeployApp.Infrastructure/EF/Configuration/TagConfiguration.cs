using DeployApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeployApp.Infrastructure.EF.Configuration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("tag_010");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasColumnName("id_010")
                .HasColumnOrder(0);
            builder.Property(t => t.Name)
                .HasColumnName("name_010")
                .HasColumnOrder(1)
                .IsRequired();
            builder.HasIndex(t => t.Name)
                .IsUnique();
            builder.Property(t => t.Description)
                .HasColumnName("description_010")
                .HasColumnOrder (2);
        }
    }
}
