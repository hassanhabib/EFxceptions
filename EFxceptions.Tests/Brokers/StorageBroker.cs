// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using EFxceptions.Extensions;
using EFxceptions.Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Tests.Brokers
{
    internal class StorageBroker : EFxceptionsContext
    {
        public DbSet<SomeEntity> SomeEntities { get; set; }
        public DbSet<SomeOtherEntity> SomeOtherEntitys { get; set; }

        public StorageBroker(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureTemporalTable<SomeEntity>();

            modelBuilder.ConfigureTemporalTable<SomeOtherEntity>(
                tableName: "AlsoSomeEntities");
        }
    }
}
