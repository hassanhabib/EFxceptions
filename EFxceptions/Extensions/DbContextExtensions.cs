// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring and querying temporal tables in Entity Framework.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Configures a temporal table for the specified entity type, with an optional table name.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to configure.</typeparam>
        /// <param name="modelBuilder">The <see cref="ModelBuilder"/> used to configure the model.</param>
        /// <param name="tableName">The name of the table (optional). Defaults to the pluralized entity name.</param>
        public static void ConfigureTemporalTable<TEntity>(
            this ModelBuilder modelBuilder,
            string tableName = null)
            where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                tableName = $"{typeof(TEntity).Name}s";
            }

            modelBuilder.Entity<TEntity>()
                .ToTable(tableName, table => table.IsTemporal());
        }

        /// <summary>
        /// Retrieves the entire history of a temporal table for the specified entity type,
        /// ordered by end date descending. Current version is included.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to query.</typeparam>
        /// <param name="dbSet">The <see cref="DbSet{TEntity}"/> to query.</param>
        /// <returns>
        /// An <see cref="IQueryable{TEntity}"/> containing the historical records,
        /// ordered by the "PeriodEnd" column descending, excluding the current version.
        /// </returns>
        public static IOrderedQueryable<TEntity> SelectAllEntityHistories<TEntity>(
            this DbSet<TEntity> dbSet) where TEntity : class
        {
            return dbSet.TemporalAll()
                .OrderByDescending(entity =>
                    EF.Property<DateTime>(entity, "PeriodEnd"));
        }
    }
}
