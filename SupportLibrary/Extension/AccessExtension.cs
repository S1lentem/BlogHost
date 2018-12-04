using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupportLibrary.Extension
{
    public static class AccessExtension
    {
        public static bool CheckRole(Func<string, bool> func, params string[] roles)
        {
            foreach (var role in roles)
            {
                if (func(role))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsOneOf<T>(this T self, params T[] collection) => collection.Contains(self);
    }
}
