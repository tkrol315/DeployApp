using DeployApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeployApp.Infrastructure.EF.Configuration
{
    public class InstanceGroupConfiguration : IEntityTypeConfiguration<InstanceGroup>
    {
        public void Configure(EntityTypeBuilder<InstanceGroup> builder)
        {
            builder.ToTable("instance_group_006");
            builder.HasKey(ig => new { ig.InstanceId, ig.GroupId });
            builder.Property(ig => ig.InstanceId)
                .HasColumnName("id_004_006")
                .HasColumnOrder(0);
            builder.Property(ig => ig.GroupId)
                .HasColumnName("id_011_006")
                .HasColumnOrder(1);
            builder.HasOne(ig => ig.Instance)
                .WithMany(i => i.InstanceGroups)
                .HasForeignKey(ig => ig.InstanceId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(ig => ig.Group)
                .WithMany(g => g.InstanceGroups)
                .HasForeignKey(ig => ig.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
