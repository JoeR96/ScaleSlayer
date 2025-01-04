﻿using Microsoft.EntityFrameworkCore;
using ScaleSlayer.Domain.Common.Interfaces;
using ScaleSlayer.Domain.UserAggregate.ValueObjects;

namespace ScaleSlayer.Infrastructure.Persistence.Configuration;

internal static class AuditableConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IAuditable).IsAssignableFrom(entity.ClrType))
            {
                var createdByUserIdProperty = entity.ClrType.GetProperty(nameof(IAuditable.CreatedByUserId))!.Name;
                var createdByProperty = entity.ClrType.GetProperty(nameof(IAuditable.CreatedBy))!.Name;
                var lastModifiedByUserIdProperty = entity.ClrType.GetProperty(nameof(IAuditable.LastModifiedByUserId))!.Name;
                var lastModifiedByProperty = entity.ClrType.GetProperty(nameof(IAuditable.LastModifiedBy))!.Name;

                modelBuilder.Entity(entity.ClrType)
                    .Property<UserId>(createdByUserIdProperty)
                    .HasConversion(id => id.Value, value => UserId.Create(value));
                modelBuilder.Entity(entity.ClrType)
                    .HasOne(createdByProperty)
                    .WithMany()
                    .HasForeignKey(createdByUserIdProperty);

                modelBuilder.Entity(entity.ClrType)
                    .Property<UserId>(lastModifiedByUserIdProperty)
                    .HasConversion(id => id.Value, value => UserId.Create(value));
                modelBuilder.Entity(entity.ClrType)
                    .HasOne(lastModifiedByProperty)
                    .WithMany()
                    .HasForeignKey(lastModifiedByUserIdProperty);
            }
        }
    }
}