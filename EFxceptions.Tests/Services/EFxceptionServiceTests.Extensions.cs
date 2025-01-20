// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Tests.Brokers;
using EFxceptions.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EFxceptions.Tests.Services
{
    public partial class EFxceptionServiceTests
    {
        [Fact]
        public void ShouldConfigureHistoryTable()
        {
            // given.. when
            var options = new DbContextOptionsBuilder<StorageBroker>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            var storageBroker = new StorageBroker(options);

            // then
            var entityType =
                storageBroker.Model.FindEntityType(typeof(SomeEntity));

            var isTemporal = entityType.IsTemporal();

            Assert.NotNull(entityType);
            Assert.True(isTemporal);
        }

        [Fact]
        public void ShouldUseCustomTableName()
        {
            // given .. when
            const string customTableName = "AlsoSomeEntities";
            var options = new DbContextOptionsBuilder<StorageBroker>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using var storageBroker = new StorageBroker(options);

            // then
            var entityType =
                storageBroker.Model.FindEntityType(typeof(SomeOtherEntity));

            Assert.NotNull(entityType);
            Assert.Equal(customTableName, entityType.GetTableName());
        }


        [Fact]
        public void ShouldUseDefaultHistoryTableName()
        {
            // given .. when
            const string expectedTableName = "SomeEntitys";
            var options = new DbContextOptionsBuilder<StorageBroker>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using var storageBroker = new StorageBroker(options);

            // then
            var acturalEntityType =
                storageBroker.Model.FindEntityType(typeof(SomeEntity));

            Assert.NotNull(acturalEntityType);

            Assert.Equal(
                expectedTableName,
                acturalEntityType.GetTableName());
        }
    }
}