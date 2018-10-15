using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustReadMe.Models
{
    public static class SampleData
    {
        public static void Initialize(BloghostContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new UserModel
                    {
                        Nickname = "S1lentem",
                        Email = "S1lentem@gmail.com",
                        Password = "11223344"
                    },
                    new UserModel
                    {
                        Nickname = "Tema",
                        Email = "tema@mail.ru",
                        Password = "t3ma1"
                    });
                context.SaveChanges();
            }
        }
    }
}
