using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustReadMe.Protection
{
    interface IHashable
    {
        string GetHash();
        bool VerifyPassword(string haskToCheck);
    }
}
