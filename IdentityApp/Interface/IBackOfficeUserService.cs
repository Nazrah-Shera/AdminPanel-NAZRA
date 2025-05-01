using IdentityApp.Models;
using IdentityApp.ViewModels;

namespace IdentityApp.Interface
{
    public interface IBackOfficeUserService
    {
        void CreateUser(vm_AddUser model, string identityUserId);
        IEnumerable<TBackOfficeUser> GetUsers();
        bool IsEmailExists(string email);
        bool UpdateUser(vm_AddUser model);
    }
}
