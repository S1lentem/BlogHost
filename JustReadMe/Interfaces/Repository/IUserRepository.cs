using JustReadMe.Models;
using JustReadMe.ViewModels;
using System.Threading.Tasks;

namespace JustReadMe.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        void Add(UserModel model);

        UserModel GetByName(string name);
        UserModel GetByEmail(string email);
    }
}
