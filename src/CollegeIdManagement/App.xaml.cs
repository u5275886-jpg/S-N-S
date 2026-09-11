using System.Windows;
using CollegeIdManagement.Data;
using CollegeIdManagement.Views.Login;

namespace CollegeIdManagement
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize EF Core SQLite DB and Seeders
            DatabaseInitializer.Initialize();

            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}
