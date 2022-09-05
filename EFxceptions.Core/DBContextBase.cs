// ---------------------------------------------------------------
// Copyright (c) H. Habib, A. Luo, S. Weitzhandler & M. Mahdhi
// All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace EFxceptions.Core
{
    public abstract class DBContextBase<TDbException> : DbContext
        where TDbException : DbException
    {

    }
}
