﻿// ---------------------------------------------------------------
// Copyright (c) Hassan Habib, Alice Luo and Shimmy Weitzhandler  All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Services
{
    public interface IEFxceptionService
    {
        void ThrowMeaningfulException(DbUpdateException dbUpdateException);
    }
}
