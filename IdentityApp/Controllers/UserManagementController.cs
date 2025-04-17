using IdentityApp.Database;
using IdentityApp.Interface;
using IdentityApp.Models;
using IdentityApp.Services;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserService _applicationUserService;

        public UserManagementController(ApplicationDbContext context, IApplicationUserService applicationUserService)
        {
            _context = context;
            _applicationUserService = applicationUserService;
        }

        public IActionResult Index() => View();


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult AddUser(vm_AddUser model)
        {
            if (ModelState.IsValid)
            {
                _applicationUserService.CreateUser(model);
                TempData["Success"] = "User added successfully!";
                return RedirectToAction("UserList");
            }

            return View(model);
        }

        public IActionResult UserList()
        {
            var users = _applicationUserService.GetUsers();
            return View(users);
        }

    }
}
