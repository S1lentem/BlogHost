using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<UserModel> Users { get; set; }

        public Role() => Users = new List<UserModel>();
    }
}
