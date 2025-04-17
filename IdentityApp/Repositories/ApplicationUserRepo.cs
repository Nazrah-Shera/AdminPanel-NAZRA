using IdentityApp.Database;
using IdentityApp.Interface;
using IdentityApp.Models;
using IdentityApp.ViewModels;

namespace IdentityApp.Repositories
{
    public class ApplicationUserRepo : IApplicationUserRepo
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUser(TApplicationUser user)
        {
            _context.T_ApplicationUsers.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<TApplicationUser> GetAllUsers()
        {
            return _context.T_ApplicationUsers.ToList();
        }

    }
}
