// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace EFxceptions.Services
{
    public interface IEFxceptionService
    {
        void ThrowMeaningfulException(DbUpdateException dbUpdateException);
    }
}
