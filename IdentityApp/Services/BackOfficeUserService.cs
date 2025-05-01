using IdentityApp.Interface;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace IdentityApp.Services
{
    public class BackOfficeUserService : IBackOfficeUserService
    {
        private readonly IBackOfficeUserRepo _backOfficeUserRepo;

        public BackOfficeUserService(IBackOfficeUserRepo backOfficeUserRepo)
        {
            _backOfficeUserRepo = backOfficeUserRepo;
        }

        public void CreateUser(vm_AddUser model, string identityUserId)
        {

            var userEntity = new TBackOfficeUser
            {
                UserID = identityUserId,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsActive = true,
                CreatedOn = DateTime.Now,
                // Hash the password if needed before saving
            };


            _backOfficeUserRepo.AddUser(userEntity);
        }

        public IEnumerable<TBackOfficeUser> GetUsers()
        {
            return _backOfficeUserRepo.GetAllUsers();
        }



        public bool IsEmailExists(string email)
        {
            return _backOfficeUserRepo.IsEmailExists(email);

        }


        public bool UpdateUser(vm_AddUser model)
        {
            try
            {
                var user = _backOfficeUserRepo.GetUserByUserId(model.UserID);
                if (user == null)
                    return false;

                // Update fields
                user.FullName = model.FullName;
                user.PhoneNumber = model.PhoneNumber;
                user.MaxRolesAllowed = model.MaxRolesAllowed;
                user.IsActive = model.IsActive;

                // Optional audit fields
                //user.UpdatedOn = DateTime.Now;
                //user.UpdatedBy = "system"; // Replace with actual user ID

                _backOfficeUserRepo.UpdateUser(user);
                return true;
            }
            catch (Exception)
            {
                // Log if needed
                return false;
            }
        }

    }
}
