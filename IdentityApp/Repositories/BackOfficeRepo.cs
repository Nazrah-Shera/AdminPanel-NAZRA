using IdentityApp.Database;
using IdentityApp.Interface;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Repositories
{
    public class BackOfficeRepo : IBackOfficeUserRepo
    {
        private readonly ApplicationDbContext _context;

        public BackOfficeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUser(TBackOfficeUser user)
        {
            _context.T_backOfficeUsers.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<TBackOfficeUser> GetAllUsers()
        {
            return _context.T_backOfficeUsers.ToList();
        }


        public bool IsEmailExists(string email)
        {
            return _context.T_backOfficeUsers.Any(u => u.Email.ToLower() == email.ToLower());

        }

        public TBackOfficeUser GetUserByUserId(string UserId)
        {
            var user = _context.T_backOfficeUsers.SingleOrDefault(u => u.UserID == UserId);
            if (user == null)
                throw new Exception("User not found.");
            return user;
        }

        public void UpdateUser(TBackOfficeUser user)
        {
            _context.T_backOfficeUsers.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(string userId)
        {
            var user = _context.T_backOfficeUsers.FirstOrDefault(u => u.UserID == userId);
            if (user != null)
            {
                _context.T_backOfficeUsers.Remove(user);
                _context.SaveChanges();
            }
        }

    }
}
