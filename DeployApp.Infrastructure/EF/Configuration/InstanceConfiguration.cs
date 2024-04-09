﻿using DeployApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeployApp.Infrastructure.EF.Configuration
{
    public class InstanceConfiguration : IEntityTypeConfiguration<Instance>
    {
        public void Configure(EntityTypeBuilder<Instance> builder)
        {
            builder.ToTable("instance_004");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasColumnName("id_004")
                .HasColumnOrder(0);
            builder.Property(i => i.ProjectId)
                .HasColumnName("id_001_004")
                .HasColumnOrder(1);
            builder.Property(i => i.TypeId)
                .HasColumnName("id_003_004")
                .HasColumnOrder(2);
            builder.Property(i => i.Key)
                .HasColumnName("key_004")
                .HasColumnOrder(3)
                .IsRequired();
            builder.Property(i => i.Secret)
                .HasColumnName("secret_004")
                .HasColumnOrder(4)
                .IsRequired();
            builder.Property(i => i.ProjectVersionId)
                .HasColumnName("id_002_actual_004")
                .HasColumnOrder(5);
            builder.HasOne(i => i.Project)
                .WithMany(p => p.Instances)
                .HasForeignKey(i => i.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(i => i.Type)
                .WithOne(t => t.Instance)
                .HasForeignKey<Instance>(i => i.TypeId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(i => i.ProjectVersion) 
                .WithOne() 
                .HasForeignKey<Instance>(i => i.ProjectVersionId) 
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
