using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicationProcess.December2020.Domain.Types
{
    [Flags]
    public enum ApplicationEntityState
    {
        IsActive = 1,
        IsConfirmed = 2,
        IsFixed = 4,
        IsDeleted = 8,
        IsSystematic = 16,
        IsUserCustom = 32
    }
}
