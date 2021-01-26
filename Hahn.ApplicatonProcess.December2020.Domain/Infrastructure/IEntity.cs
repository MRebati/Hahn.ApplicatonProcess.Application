using System;
using Hahn.ApplicationProcess.December2020.Domain.Types;

namespace Hahn.ApplicationProcess.December2020.Domain.Infrastructure
{
    public interface IEntity
    {
        Object Id { get; set; }

        ApplicationEntityState EntityState { get; set; }
    }
}