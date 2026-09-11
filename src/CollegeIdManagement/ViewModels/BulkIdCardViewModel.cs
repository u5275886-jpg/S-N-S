using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CollegeIdManagement.Helpers;
using CollegeIdManagement.Services;

namespace CollegeIdManagement.ViewModels
{
    public class BulkIdCardViewModel : ObservableObject
    {
        private readonly StudentService _studentService = new();
        private readonly PdfService _pdfService = new();

        public ObservableCollection<string> Sessions { get; } = new() { "All Sessions", "2023-24", "2024-25", "2025-26", "2026-27" };
        public ObservableCollection<string> Courses { get; } = new() { "All Courses", "B.A", "B.Sc", "B.Com", "M.A" };

        private string _selectedSession = "2026-27";
        public string SelectedSession
        {
            get => _selectedSession;
            set => SetProperty(ref _selectedSession, value);
        }

        private string _selectedCourse = "All Courses";
        public string SelectedCourse
        {
            get => _selectedCourse;
            set => SetProperty(ref _selectedCourse, value);
        }

        private int _progressValue;
        public int ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        private int _maxProgress = 100;
        public int MaxProgress
        {
            get => _maxProgress;
            set => SetProperty(ref _maxProgress, value);
        }

        private string _statusText = "Ready to generate bulk ID cards.";
        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        private bool _isGenerating;
        public bool IsGenerating
        {
            get => _isGenerating;
            set => SetProperty(ref _isGenerating, value);
        }

        public RelayCommand GenerateBulkCommand { get; }

        public BulkIdCardViewModel()
        {
            GenerateBulkCommand = new RelayCommand(ExecuteGenerateBulk);
        }

        private async void ExecuteGenerateBulk()
        {
            var students = _studentService.FilterStudents(null, SelectedSession, SelectedCourse, null, null);
            if (!students.Any())
            {
                DialogHelper.ShowWarning("No students match the selected session and course filter.", "No Records");
                return;
            }

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"Bulk_ID_Cards_{SelectedSession.Replace("-", "_")}_{SelectedCourse}.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                IsGenerating = true;
                MaxProgress = students.Count;
                ProgressValue = 0;
                StatusText = $"Generating 0 of {students.Count} ID cards...";

                var targetPath = saveFileDialog.FileName;

                await Task.Run(() =>
                {
                    _pdfService.GenerateBulkStudentsIdPdf(students, targetPath);
                    foreach (var s in students)
                    {
                        _studentService.MarkIdGenerated(s.Id);
                    }
                });

                ProgressValue = MaxProgress;
                StatusText = $"Successfully generated {students.Count} ID cards!";
                IsGenerating = false;

                DialogHelper.ShowInfo($"Bulk PDF generated successfully!\nFile: {targetPath}", "Bulk Complete");
            }
        }
    }
}
