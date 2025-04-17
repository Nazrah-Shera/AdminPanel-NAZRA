using IdentityApp.Models;
using IdentityApp.ViewModels;

namespace IdentityApp.Interface
{
    public interface  IApplicationUserRepo
    {
        void AddUser(TApplicationUser user);
        IEnumerable<TApplicationUser> GetAllUsers();
    }
}
