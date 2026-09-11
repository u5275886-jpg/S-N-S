using System.Collections.ObjectModel;
using System.Linq;
using CollegeIdManagement.Helpers;
using CollegeIdManagement.Models;
using CollegeIdManagement.Services;

namespace CollegeIdManagement.ViewModels
{
    public class IdCardViewModel : ObservableObject
    {
        private readonly StudentService _studentService = new();
        private readonly PdfService _pdfService = new();

        public ObservableCollection<Student> Students { get; } = new();

        private Student? _selectedStudent;
        public Student? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                if (SetProperty(ref _selectedStudent, value))
                {
                    CardWrapper = _selectedStudent != null ? new IdCardTemplateWrapper(_selectedStudent) : null;
                }
            }
        }

        private IdCardTemplateWrapper? _cardWrapper;
        public IdCardTemplateWrapper? CardWrapper
        {
            get => _cardWrapper;
            set => SetProperty(ref _cardWrapper, value);
        }

        public RelayCommand LoadStudentsCommand { get; }
        public RelayCommand PreviewCardCommand { get; }
        public RelayCommand ExportPdfCommand { get; }

        public IdCardViewModel()
        {
            LoadStudentsCommand = new RelayCommand(LoadStudents);
            PreviewCardCommand = new RelayCommand(ExecutePreviewCard);
            ExportPdfCommand = new RelayCommand(ExecuteExportPdf);

            LoadStudents();
        }

        public void LoadStudents()
        {
            Students.Clear();
            var list = _studentService.GetAllStudents();
            foreach (var s in list) Students.Add(s);

            if (Students.Any()) SelectedStudent = Students.First();
        }

        private void ExecutePreviewCard()
        {
            if (SelectedStudent == null)
            {
                DialogHelper.ShowWarning("Please select a student to preview.", "Selection Needed");
                return;
            }

            var previewWin = new Views.IdCards.IdCardPreviewView(SelectedStudent);
            previewWin.ShowDialog();
        }

        private void ExecuteExportPdf()
        {
            if (SelectedStudent == null)
            {
                DialogHelper.ShowWarning("Please select a student.", "Selection Needed");
                return;
            }

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                FileName = $"ID_{SelectedStudent.RollNo}_{SelectedStudent.Name.Replace(" ", "_")}.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                _pdfService.GenerateSingleStudentIdPdf(SelectedStudent, saveFileDialog.FileName);
                _studentService.MarkIdGenerated(SelectedStudent.Id);
                DialogHelper.ShowInfo("PDF generated successfully!", "Success");
            }
        }
    }
}
