using System.Collections.ObjectModel;
using System.Linq;
using CollegeIdManagement.Data;
using CollegeIdManagement.Helpers;
using CollegeIdManagement.Models;
using CollegeIdManagement.Services;

namespace CollegeIdManagement.ViewModels
{
    public class UserManagementViewModel : ObservableObject
    {
        public ObservableCollection<User> Users { get; } = new();

        public RelayCommand AddUserCommand { get; }

        public UserManagementViewModel()
        {
            AddUserCommand = new RelayCommand(ExecuteAddUser);
            LoadUsers();
        }

        public void LoadUsers()
        {
            Users.Clear();
            using var context = new AppDbContext();
            var list = context.Users.ToList();
            foreach (var u in list) Users.Add(u);
        }

        private void ExecuteAddUser()
        {
            using var context = new AppDbContext();
            var newUsername = $"operator_{Users.Count + 1}";
            if (!context.Users.Any(u => u.Username == newUsername))
            {
                var newUser = new User
                {
                    Username = newUsername,
                    PasswordHash = DbSeeder.HashPassword("operator123"),
                    FullName = $"Operator {Users.Count + 1}",
                    Email = $"{newUsername}@snscollege.ac.in",
                    Role = UserRole.Operator,
                    IsActive = true
                };
                context.Users.Add(newUser);
                context.SaveChanges();
                AuditService.Log(AuthenticationService.CurrentUser?.Username ?? "Admin", "User Created", "User", $"Created user account '{newUsername}'.");
                LoadUsers();
                DialogHelper.ShowInfo($"Created new operator account: {newUsername} (Password: operator123)", "User Added");
            }
        }
    }
}
