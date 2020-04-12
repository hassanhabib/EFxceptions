// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace EFxceptions
{
    public class EFxceptionsContext : DbContext
    {
        public EFxceptionsContext(DbContextOptions options) : base(options) { }
    }
}
