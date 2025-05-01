using IdentityApp.Areas.Identity;
using IdentityApp.Database;
using IdentityApp.Interface;
using IdentityApp.Models;
using IdentityApp.Services;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text.Json;

namespace IdentityApp.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBackOfficeUserService _backOfficeUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LoggingService _loggingService;

        public UserManagementController(ApplicationDbContext context, IBackOfficeUserService backOfficeUserService, UserManager<ApplicationUser> userManager,
            LoggingService loggingService)
        {
            _context = context;
            _backOfficeUserService = backOfficeUserService;
            _userManager = userManager;
            _loggingService = loggingService;
        }

        public IActionResult Index() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(vm_AddUser model)
        {
          
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, message = "Invalid data submitted." });

                // Check if email already exists in BackOfficeUser Table
                //bool exists = await _applicationUserService.IsEmailExists(model.Email);
                //if (exists)
                //    return Json(new { success = false, message = $"Email '{model.Email}' is already taken." });

                // Check if email already exists in Identity
                var identityUserExists = await _userManager.FindByEmailAsync(model.Email);
                if (identityUserExists != null)
                    return Json(new { success = false, message = $"Email '{model.Email}' is already taken." });

                // Create Identity user
                var identityUser = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    //FullName = model.FullName // if you have a custom property
                };

                var result = await _userManager.CreateAsync(identityUser, model.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return Json(new { success = false, message = "Identity error: " + errors });
                }

                // ✅ Then create in your custom `t_applicationusers`
                _backOfficeUserService.CreateUser(model, identityUser.Id);
                // Example: Log a system event
                _loggingService.LogApplication(identityUser.Id, "Success", "UserManagement", "AddUser", "User added successfully!","","", JsonSerializer.Serialize(model) ,"Success");

                return Json(new { success = true, message = "User added successfully!" });

            }
            catch (Exception ex)
            {
                // Log the exception
                _loggingService.LogApplication(
                    userId: "",
                    logLevel: "Exception",
                    module: "UserManagement",
                    action: "AddUser",
                    message: "Exception occurred while adding user.",
                    exception: ex.ToString(),
                    ipAddress:"",
                    requestParameters:"",
                    responseParameters:""
                    );
                return Json(new { success = false, message = "An unexpected error occurred. Please try again later." });
            }




        }
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _backOfficeUserService.GetUsers();

                var result = users.Select(u => new
                {
                    userId = u.UserID,
                    fullName = u.FullName,
                    email = u.Email,
                    phoneNumber = u.PhoneNumber,
                    maxRolesAllowed = u.MaxRolesAllowed,
                    isActive = u.IsActive
                });

                return Json(result);
            }
            catch (Exception ex)
            {
                _loggingService.LogApplication(
                    userId: "",
                    logLevel: "Exception",
                    module: "UserManagement",
                    action: "AddUser",
                    message: "Exception occurred while adding user.",
                    exception: ex.ToString(),
                    ipAddress: "",
                    requestParameters: "",
                    responseParameters: ""
                );

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(vm_AddUser model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new { success = false, message = "Invalid data submitted." });

                // Step 1: Find the Identity user by ID
                var identityUser = await _userManager.FindByIdAsync(model.UserID);
                if (identityUser == null)
                    return Json(new { success = false, message = "User not found in Identity." });

                // Optional: update email or username if editable
                identityUser.UserName = model.Email;
                identityUser.Email = model.Email;

                var updateResult = await _userManager.UpdateAsync(identityUser);
                if (!updateResult.Succeeded)
                {
                    var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                    return Json(new { success = false, message = "Identity update error: " + errors });
                }

                // Step 2: Update your custom application user table
                var updateSuccess = _backOfficeUserService.UpdateUser(model);
                if (!updateSuccess)
                    return Json(new { success = false, message = "Failed to update user in application table." });

                // Step 3: Log success
                _loggingService.LogApplication(identityUser.Id, "Success", "UserManagement", "EditUser", "User updated successfully", "", "", JsonSerializer.Serialize(model), "Success");

                return Json(new { success = true, message = "User updated successfully!" });
            }
            catch (Exception ex)
            {
                _loggingService.LogApplication(
                    userId: model.UserID,
                    logLevel: "Exception",
                    module: "UserManagement",
                    action: "EditUser",
                    message: "Exception occurred while editing user.",
                    exception: ex.ToString(),
                    ipAddress: "",
                    requestParameters: "",
                    responseParameters: ""
                );

                return Json(new { success = false, message = "An unexpected error occurred. Please try again later." });
            }
        }

    }
}
