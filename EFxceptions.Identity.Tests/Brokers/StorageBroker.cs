// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using EFxceptions.Identity.Tests.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Identity.Tests.Brokers
{
    internal class StorageBroker : EFxceptionsIdentityContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<SomeEntity> SomeEntities { get; set; }
        public DbSet<SomeOtherEntity> SomeOtherEntitys { get; set; }

        public StorageBroker(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SomeEntity>()
                .ToTable("SomeEntitys", table => table.IsTemporal());

            modelBuilder.Entity<SomeOtherEntity>()
                .ToTable("AlsoSomeEntities", table => table.IsTemporal());
        }
    }
}
