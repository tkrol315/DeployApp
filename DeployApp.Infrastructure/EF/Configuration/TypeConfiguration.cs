using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeployApp.Infrastructure.EF.Configuration
{
    public class TypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Type>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Type> builder)
        {
            builder.ToTable("instance_type_003");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasColumnName("id_003")
                .HasColumnOrder(0);
            builder.Property(t => t.Description)
                .HasColumnName("description_003")
                .HasColumnType("varchar(250)")
                .HasColumnOrder(1)
                .IsRequired();
        }
    }
}
