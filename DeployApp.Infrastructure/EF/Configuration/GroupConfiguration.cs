﻿using DeployApp.Domain.Entities;
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
                .HasColumnOrder(1);
            builder.Property(g => g.Description)
                .HasColumnName("description_011")
                .HasColumnOrder (2);
        }
    }
}