// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers.DbErrors;
using Npgsql;

namespace EFxceptions.PosgreSQL.Brokers
{
    public interface IPosgreSqlErrorBroker : IDbErrorBroker<PostgresException,string>
    { }
}
