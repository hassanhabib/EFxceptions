// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.Data.SqlClient;

namespace EFxceptions.Brokers
{
    public interface ISqlErrorBroker
    {
        int GetSqlErrorCode(SqlException sqlException);
    }
}
