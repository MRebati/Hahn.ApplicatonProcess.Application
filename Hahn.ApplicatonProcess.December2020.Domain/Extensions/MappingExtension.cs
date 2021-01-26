using System;
using System.Collections.Generic;
using System.Text;
using Mapster;

namespace Hahn.ApplicationProcess.December2020.Domain.Extensions
{
    public static class MappingExtension
    {
        public static TTarget MapTo<TTarget>(this object source)
        {
            return source.Adapt<TTarget>();
        }

        public static List<TTarget> MapToList<TTarget>(this object source)
        {
            return source.Adapt<List<TTarget>>();
        }
    }
}
