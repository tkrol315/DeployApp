using DeployApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeployApp.Infrastructure.EF.Configuration
{
    public class InstanceTagConfiguration : IEntityTypeConfiguration<InstanceTag>
    {
        public void Configure(EntityTypeBuilder<InstanceTag> builder)
        {
            builder.ToTable("instance_tag_005");
            builder.HasKey(it => new { it.InstanceId, it.TagId });
            builder.Property(it => it.InstanceId)
                .HasColumnType("uuid")
                .HasColumnName("id_004_005")
                .HasColumnOrder(0);
            builder.Property(it => it.TagId)
                .HasColumnName("id_010_005")
                .HasColumnOrder(1);
            builder.HasOne(it => it.Instance)
                .WithMany(i => i.InstanceTags)
                .HasForeignKey(it => it.InstanceId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(it => it.Tag)
                .WithMany(t => t.InstanceTags)
                .HasForeignKey(it => it.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
