using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustReadMe.Interfaces
{
    public interface IHashable
    {
        string GetHash();
        bool VerifyPassword(string haskToCheck);
        string Password { get; set; }
    }
}
