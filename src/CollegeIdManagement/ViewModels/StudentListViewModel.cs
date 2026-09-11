using System;
using System.Collections.ObjectModel;
using System.Linq;
using CollegeIdManagement.Helpers;
using CollegeIdManagement.Models;
using CollegeIdManagement.Services;

namespace CollegeIdManagement.ViewModels
{
    public class StudentListViewModel : ObservableObject
    {
        private readonly MainViewModel _mainVM;
        private readonly StudentService _studentService = new();
        private readonly ExcelService _excelService = new();

        public ObservableCollection<Student> Students { get; } = new();

        private string _searchTerm = string.Empty;
        public string SearchTerm
        {
            get => _searchTerm;
            set { SetProperty(ref _searchTerm, value); Search(); }
        }

        private string _selectedSession = "All Sessions";
        public string SelectedSession
        {
            get => _selectedSession;
            set { SetProperty(ref _selectedSession, value); Search(); }
        }

        private string _selectedCourse = "All Courses";
        public string SelectedCourse
        {
            get => _selectedCourse;
            set { SetProperty(ref _selectedCourse, value); Search(); }
        }

        public ObservableCollection<string> Sessions { get; } = new() { "All Sessions", "2023-24", "2024-25", "2025-26", "2026-27" };
        public ObservableCollection<string> Courses { get; } = new() { "All Courses", "B.A", "B.Sc", "B.Com", "M.A" };

        public RelayCommand AddStudentCommand { get; }
        public RelayCommand EditStudentCommand { get; }
        public RelayCommand DeleteStudentCommand { get; }
        public RelayCommand ImportExcelCommand { get; }
        public RelayCommand ExportExcelCommand { get; }

        public StudentListViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;

            AddStudentCommand = new RelayCommand(ExecuteAddStudent);
            EditStudentCommand = new RelayCommand(ExecuteEditStudent);
            DeleteStudentCommand = new RelayCommand(ExecuteDeleteStudent);
            ImportExcelCommand = new RelayCommand(ExecuteImportExcel);
            ExportExcelCommand = new RelayCommand(ExecuteExportExcel);

            LoadStudents();
        }

        public void LoadStudents()
        {
            Search();
        }

        public void Search()
        {
            Students.Clear();
            var list = _studentService.FilterStudents(SearchTerm, SelectedSession, SelectedCourse, null, null);
            foreach (var s in list)
            {
                Students.Add(s);
            }
        }

        private void ExecuteAddStudent()
        {
            var vm = new StudentViewModel();
            var formWin = new Views.Students.StudentFormView { DataContext = vm };
            vm.OnSaved += () => { formWin.Close(); LoadStudents(); };
            formWin.ShowDialog();
        }

        private void ExecuteEditStudent(object? parameter)
        {
            if (parameter is Student s)
            {
                var vm = new StudentViewModel(s);
                var formWin = new Views.Students.StudentFormView { DataContext = vm };
                vm.OnSaved += () => { formWin.Close(); LoadStudents(); };
                formWin.ShowDialog();
            }
        }

        private void ExecuteDeleteStudent(object? parameter)
        {
            if (parameter is Student s)
            {
                if (DialogHelper.Confirm($"Are you sure you want to delete student '{s.Name}' ({s.MemberCode})?", "Confirm Delete"))
                {
                    _studentService.DeleteStudent(s.Id);
                    LoadStudents();
                }
            }
        }

        private void ExecuteImportExcel()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var (imported, skipped, errors) = _excelService.ImportStudentsFromExcel(openFileDialog.FileName);
                DialogHelper.ShowInfo($"Excel Import Completed.\nImported: {imported}\nSkipped/Errors: {skipped}", "Import Result");
                LoadStudents();
            }
        }

        private void ExecuteExportExcel()
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = $"Students_Export_{DateTime.Now:yyyyMMdd}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                _excelService.ExportStudentsToExcel(Students.ToList(), saveFileDialog.FileName);
                DialogHelper.ShowInfo("Students exported to Excel successfully!", "Export Success");
            }
        }
    }
}
