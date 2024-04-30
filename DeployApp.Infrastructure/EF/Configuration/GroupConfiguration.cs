using DeployApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeployApp.Infrastructure.EF.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("group_011");
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id)
                .HasColumnName("id_011")
                .HasColumnOrder(0);
            builder.Property(g => g.Name)
                .HasColumnName("name_011")
                .HasColumnType("varchar(100)")
                .HasColumnOrder(1)
                .IsRequired();
            builder.HasIndex(g => g.Name)
              .IsUnique();
            builder.Property(g => g.Description)
                .HasColumnName("description_011")
                .HasColumnType("varchar(250)")
                .HasColumnOrder (2);
        }
    }
}
