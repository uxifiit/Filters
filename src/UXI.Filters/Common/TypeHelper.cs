using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UXI.Filters.Common
{
    public static class TypeCompatibilityHelper
    {
        public static bool CanAssign(Type sourceType, Type targetType)
        {
            return sourceType == targetType
                   || sourceType.IsSubclassOf(targetType)
                   || targetType.IsAssignableFrom(sourceType);
        }
    }
}
