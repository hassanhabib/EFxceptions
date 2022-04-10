// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using EFxceptions.Brokers;
using Microsoft.Data.SqlClient;

namespace EFxceptions.Identity.Brokers
{
    public interface ISqlErrorBroker : IDbErrorBroker<SqlException>
    {
    }
}
