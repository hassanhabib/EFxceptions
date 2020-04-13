// ---------------------------------------------------------------
// Copyright (c) Hassan Habib & Alice Luo  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Services
{
    public interface IEFxceptionService
    {
        void ThrowMeaningfulException(DbUpdateException dbUpdateException);
    }
}
