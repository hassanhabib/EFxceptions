using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Configures a temporal table for the specified entity type, with an optional table name.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to configure.</typeparam>
        /// <param name="modelBuilder">The <see cref="ModelBuilder"/> used to configure the model.</param>
        /// <param name="tableName">The name of the table (optional). Defaults to the pluralized entity name.</param>
        public static void ConfigureHistoryTable<TEntity>(
            this ModelBuilder modelBuilder,
            string tableName = "")
            where TEntity : class
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                tableName = $"{typeof(TEntity).Name}s";
            }

            modelBuilder.Entity<TEntity>()
                .ToTable($"{typeof(TEntity).Name}s", table => table.IsTemporal());
        }

        /// <summary>
        /// Retrieves the entire history of a temporal table for the specified entity type,
        /// ordered by end date descending.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to query.</typeparam>
        /// <param name="dbSet">The <see cref="DbSet{TEntity}"/> to query.</param>
        /// <returns>
        /// An <see cref="IQueryable{TEntity}"/> containing the historical records,
        /// ordered by the "PeriodEnd" column descending, excluding the current version.
        /// </returns>
        public static IQueryable<TEntity> SelectAllEntityHistorys<TEntity>(
            this DbSet<TEntity> dbSet) where TEntity : class
        {
            return dbSet.TemporalAll()
                .OrderByDescending(entity =>
                    EF.Property<DateTime>(entity, "PeriodEnd"))
                .Skip(1);
        }
    }
}
