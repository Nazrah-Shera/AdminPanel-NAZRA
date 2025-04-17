using IdentityApp.Interface;
using IdentityApp.Models;
using IdentityApp.ViewModels;

namespace IdentityApp.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepo _applicationUserRepo;

        public ApplicationUserService(IApplicationUserRepo userRepository)
        {
            _applicationUserRepo = userRepository;
        }

        public void CreateUser(vm_AddUser model)
        {

            var userEntity = new TApplicationUser
            {
                UserID = Guid.NewGuid(),
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsActive = true,
                CreatedOn = DateTime.Now,
                // Hash the password if needed before saving
            };

    
            _applicationUserRepo.AddUser(userEntity);
        }

        public IEnumerable<TApplicationUser> GetUsers()
        {
            return _applicationUserRepo.GetAllUsers();
        }
    }
}
