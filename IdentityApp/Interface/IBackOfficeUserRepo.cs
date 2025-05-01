using IdentityApp.Models;
using IdentityApp.ViewModels;

namespace IdentityApp.Interface
{
    public interface  IBackOfficeUserRepo
    {
        void AddUser(TBackOfficeUser user);
        IEnumerable<TBackOfficeUser> GetAllUsers();
        bool IsEmailExists(string email);
        TBackOfficeUser GetUserByUserId(string UserId);
        void UpdateUser(TBackOfficeUser user);
        void DeleteUser(string userId);
    }
}
