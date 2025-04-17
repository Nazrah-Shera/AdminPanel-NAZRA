using IdentityApp.Models;
using IdentityApp.ViewModels;

namespace IdentityApp.Interface
{
    public interface IApplicationUserService
    {
        void CreateUser(vm_AddUser model);
        IEnumerable<TApplicationUser> GetUsers();
    }
}
