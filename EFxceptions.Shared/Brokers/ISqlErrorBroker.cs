﻿// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System.Data.Common;

namespace EFxceptions.Brokers
{
    public interface ISqlErrorBroker
    {
        int GetErrorCode(DbException dbException);
    }
}
