using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustReadMe.DomainModels
{
    public class UserInfo
    {
        public int Id { get; }
        public string Nicknake { get; set; }
        public string Email { get; set; }
    }
}
