using System.Collections.ObjectModel;
using System.Linq;
using CollegeIdManagement.Helpers;
using CollegeIdManagement.Models;
using CollegeIdManagement.Services;

namespace CollegeIdManagement.ViewModels
{
    public class DashboardViewModel : ObservableObject
    {
        private readonly MainViewModel _mainVM;
        private readonly StudentService _studentService = new();

        private int _totalStudents;
        public int TotalStudents { get => _totalStudents; set => SetProperty(ref _totalStudents, value); }

        private int _activeStudents;
        public int ActiveStudents { get => _activeStudents; set => SetProperty(ref _activeStudents, value); }

        private int _inactiveStudents;
        public int InactiveStudents { get => _inactiveStudents; set => SetProperty(ref _inactiveStudents, value); }

        private int _idsGenerated;
        public int IdsGenerated { get => _idsGenerated; set => SetProperty(ref _idsGenerated, value); }

        private int _idsPending;
        public int IdsPending { get => _idsPending; set => SetProperty(ref _idsPending, value); }

        public ObservableCollection<Student> RecentStudents { get; } = new();

        public RelayCommand NavigateStudentsCommand { get; }
        public RelayCommand NavigateGenerateIdCommand { get; }
        public RelayCommand NavigateBulkIdCommand { get; }
        public RelayCommand NavigateReportsCommand { get; }

        public DashboardViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;

            NavigateStudentsCommand = new RelayCommand(() => _mainVM.Navigate("Students"));
            NavigateGenerateIdCommand = new RelayCommand(() => _mainVM.Navigate("ID Cards"));
            NavigateBulkIdCommand = new RelayCommand(() => _mainVM.Navigate("Bulk Generator"));
            NavigateReportsCommand = new RelayCommand(() => _mainVM.Navigate("Reports"));

            LoadStats();
        }

        public void LoadStats()
        {
            var students = _studentService.GetAllStudents();
            TotalStudents = students.Count;
            ActiveStudents = students.Count(s => s.Status == StudentStatus.Active);
            InactiveStudents = students.Count(s => s.Status == StudentStatus.Inactive);
            IdsGenerated = students.Count(s => s.IsIdGenerated);
            IdsPending = TotalStudents - IdsGenerated;

            RecentStudents.Clear();
            foreach (var s in students.Take(5))
            {
                RecentStudents.Add(s);
            }
        }
    }
}
