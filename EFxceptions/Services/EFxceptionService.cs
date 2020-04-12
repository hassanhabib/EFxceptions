// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using EFxceptions.Brokers;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Services
{
    public class EFxceptionService : IEFxceptionService
    {
        private readonly ISqlErrorBroker sqlErrorBroker;

        public EFxceptionService(ISqlErrorBroker sqlErrorBroker) =>
            this.sqlErrorBroker = sqlErrorBroker;

        public void ThrowMeaningfulException(DbUpdateException dbUpdateException)
        {
            throw new NotImplementedException();
        }
    }
}
