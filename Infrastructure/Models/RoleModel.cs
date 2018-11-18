using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<UserModel> Users { get; set; }

        public RoleModel() => Users = new List<UserModel>();
    }
}
