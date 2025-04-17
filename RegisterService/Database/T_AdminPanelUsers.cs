using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace RegisterService.Database
{

    public class T_AdminPanelUsers
    {
        //UserID(Primary Key, INT IDENTITY(1,1)) – Unique identifier for each user
        public int UserID { get; set; }
        //FullName(NVARCHAR(100)) – User's full name
        public string FullName { get; set; }
        //Email(NVARCHAR(255)) – Email address(should be unique)
        public string Email { get; set; }
        //Username(NVARCHAR(100)) – Chosen username(should be unique)
        public string Username { get; set; }
        //PasswordHash(NVARCHAR(MAX)) – Hashed password for security
        public string PasswordHash { get; set; }
        //PhoneNumber(NVARCHAR(20)) – Contact number
        public string PhoneNumber { get; set; }

        //        Role(NVARCHAR(50)) – Defines if the user is an "Admin" or "Worker"
        public string Role { get; set; }
        //        IsActive(BIT) – Indicates whether the user account is active(1 = active, 0 = inactive)
        public bool IsActive { get; set; }
        //LastLoginDate(DATETIME) – Records the last login timestamp
        public DateTime LastLoginDate { get; set; }
        //CreatedDate(DATETIME DEFAULT GETDATE()) – Timestamp when the user was created
        public DateTime CreatedDate { get; set; }
        //CreatedBy(NVARCHAR(100)) – Who created the user(e.g., system or admin)
        public int CreatedBy { get; set; }
        //ModifiedDate(DATETIME) – Timestamp for the last modification
        public DateTime ModifiedDate { get; set; }
        //ModifiedBy(NVARCHAR(100)) – Who last modified the user
        public string ModifiedBy { get; set; }

        //        PasswordResetToken(NVARCHAR(255)) – Token for password recovery
        public string PasswordResetToken { get; set; }
        //ResetTokenExpiry(DATETIME) – Expiration time for password reset token
        public DateTime ResetTokenExpiry { get; set; }
        //FailedLoginAttempts(INT DEFAULT 0) – Tracks unsuccessful login attempts
        public int FailedLoginAttempts { get; set; }
        //LockoutEndTime(DATETIME) – If locked out, when can they try again
        public DateTime LockoutEndTime { get; set; }

        //        ProfilePicture(NVARCHAR(MAX)) – Path to profile picture if applicable
        public string ProfilePicture { get; set; }
        //Department(NVARCHAR(100)) – The department the user belongs to
        public string Department { get; set; }


    }
}
