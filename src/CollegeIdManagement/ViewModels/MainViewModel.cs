using CollegeIdManagement.Helpers;
using CollegeIdManagement.Services;

namespace CollegeIdManagement.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private object _currentView = null!;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        private string _activeMenuName = "Dashboard";
        public string ActiveMenuName
        {
            get => _activeMenuName;
            set => SetProperty(ref _activeMenuName, value);
        }

        private string _currentUserName = "Administrator";
        public string CurrentUserName
        {
            get => _currentUserName;
            set => SetProperty(ref _currentUserName, value);
        }

        private string _collegeName = "SHYAM NANDAN SAHAY COLLEGE";
        public string CollegeName
        {
            get => _collegeName;
            set => SetProperty(ref _collegeName, value);
        }

        private string _globalSearchTerm = string.Empty;
        public string GlobalSearchTerm
        {
            get => _globalSearchTerm;
            set => SetProperty(ref _globalSearchTerm, value);
        }

        public RelayCommand NavigateCommand { get; }
        public RelayCommand LogoutCommand { get; }

        public DashboardViewModel DashboardVM { get; }
        public StudentListViewModel StudentListVM { get; }
        public IdCardViewModel IdCardVM { get; }
        public BulkIdCardViewModel BulkIdCardVM { get; }
        public TemplateDesignerViewModel TemplateDesignerVM { get; }
        public ReportsViewModel ReportsVM { get; }
        public SettingsViewModel SettingsVM { get; }
        public UserManagementViewModel UserManagementVM { get; }

        public MainViewModel()
        {
            var user = AuthenticationService.CurrentUser;
            if (user != null)
            {
                CurrentUserName = $"{user.FullName} ({user.Role})";
            }

            var settingsService = new CollegeSettingsService();
            var settings = settingsService.GetSettings();
            CollegeName = settings.CollegeName;

            DashboardVM = new DashboardViewModel(this);
            StudentListVM = new StudentListViewModel(this);
            IdCardVM = new IdCardViewModel();
            BulkIdCardVM = new BulkIdCardViewModel();
            TemplateDesignerVM = new TemplateDesignerViewModel();
            ReportsVM = new ReportsViewModel();
            SettingsVM = new SettingsViewModel();
            UserManagementVM = new UserManagementViewModel();

            NavigateCommand = new RelayCommand(p => Navigate(p?.ToString()));
            LogoutCommand = new RelayCommand(ExecuteLogout);

            Navigate("Dashboard");
        }

        public void Navigate(string? viewName)
        {
            if (string.IsNullOrWhiteSpace(viewName)) return;

            ActiveMenuName = viewName;
            switch (viewName)
            {
                case "Dashboard":
                    DashboardVM.LoadStats();
                    CurrentView = DashboardVM;
                    break;
                case "Students":
                    StudentListVM.LoadStudents();
                    CurrentView = StudentListVM;
                    break;
                case "ID Cards":
                    IdCardVM.LoadStudents();
                    CurrentView = IdCardVM;
                    break;
                case "Bulk Generator":
                    CurrentView = BulkIdCardVM;
                    break;
                case "Templates":
                    CurrentView = TemplateDesignerVM;
                    break;
                case "Reports":
                    CurrentView = ReportsVM;
                    break;
                case "Settings":
                    CurrentView = SettingsVM;
                    break;
                case "Users":
                    CurrentView = UserManagementVM;
                    break;
            }
        }

        private void ExecuteLogout()
        {
            var authService = new AuthenticationService();
            authService.Logout();

            var loginWindow = new Views.Login.LoginWindow();
            loginWindow.Show();

            System.Windows.Application.Current.Windows[0]?.Close();
        }
    }
}
