using System.Windows;
using CollegeIdManagement.Helpers;
using CollegeIdManagement.Services;

namespace CollegeIdManagement.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private readonly AuthenticationService _authService = new();

        private string _username = "admin";
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public RelayCommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        private void ExecuteLogin()
        {
            ErrorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter username and password.";
                return;
            }

            if (_authService.Login(Username, Password))
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();

                Application.Current.Windows[0]?.Close();
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
            }
        }
    }
}
