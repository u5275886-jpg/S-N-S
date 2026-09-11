using System;
using System.Linq;
using CollegeIdManagement.Data;
using CollegeIdManagement.Models;

namespace CollegeIdManagement.Services
{
    public class AuthenticationService
    {
        public static User? CurrentUser { get; private set; }

        public bool Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            using var context = new AppDbContext();
            var hashedPassword = DbSeeder.HashPassword(password);

            var user = context.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower() && u.PasswordHash == hashedPassword && u.IsActive);
            if (user != null)
            {
                user.LastLoginAt = DateTime.Now;
                context.SaveChanges();

                CurrentUser = user;
                AuditService.Log(user.Username, "Login", "User", $"User '{user.Username}' logged in successfully.");
                return true;
            }

            return false;
        }

        public void Logout()
        {
            if (CurrentUser != null)
            {
                AuditService.Log(CurrentUser.Username, "Logout", "User", $"User '{CurrentUser.Username}' logged out.");
                CurrentUser = null;
            }
        }
    }
}
