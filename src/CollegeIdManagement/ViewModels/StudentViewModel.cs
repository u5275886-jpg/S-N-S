using System;
using CollegeIdManagement.Helpers;
using CollegeIdManagement.Models;
using CollegeIdManagement.Services;

namespace CollegeIdManagement.ViewModels
{
    public class StudentViewModel : ObservableObject
    {
        private readonly StudentService _studentService = new();
        private readonly StudentCodeService _codeService = new();
        private readonly RollNumberService _rollService = new();

        public Student Student { get; set; }

        public RelayCommand SaveCommand { get; }
        public RelayCommand UploadPhotoCommand { get; }
        public RelayCommand UploadSignatureCommand { get; }
        public RelayCommand AutoGenerateCodesCommand { get; }

        public event Action? OnSaved;

        public StudentViewModel(Student? student = null)
        {
            Student = student ?? new Student
            {
                Session = "2026-27",
                Course = "B.A",
                Department = "Faculty of Arts",
                District = "Muzaffarpur",
                State = "Bihar",
                Country = "India",
                Status = StudentStatus.Active,
                ValidFrom = DateTime.Now,
                ValidTill = DateTime.Now.AddYears(3)
            };

            SaveCommand = new RelayCommand(ExecuteSave);
            UploadPhotoCommand = new RelayCommand(ExecuteUploadPhoto);
            UploadSignatureCommand = new RelayCommand(ExecuteUploadSignature);
            AutoGenerateCodesCommand = new RelayCommand(ExecuteAutoGenerateCodes);

            if (student == null || string.IsNullOrWhiteSpace(Student.MemberCode))
            {
                ExecuteAutoGenerateCodes();
            }
        }

        private void ExecuteAutoGenerateCodes()
        {
            if (string.IsNullOrWhiteSpace(Student.MemberCode))
            {
                Student.MemberCode = _codeService.GenerateNextMemberCode(Student.Course, Student.Session);
                OnPropertyChanged(nameof(Student));
            }
            if (string.IsNullOrWhiteSpace(Student.RollNo))
            {
                Student.RollNo = _rollService.GenerateNextRollNumber(Student.Session, Student.Course);
                OnPropertyChanged(nameof(Student));
            }
        }

        private void ExecuteUploadPhoto()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.png;*.webp)|*.jpg;*.jpeg;*.png;*.webp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var savedPath = FileHelper.SaveStudentPhoto(openFileDialog.FileName, Student.MemberCode);
                Student.PhotoPath = savedPath;
                OnPropertyChanged(nameof(Student));
            }
        }

        private void ExecuteUploadSignature()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.jpeg;*.png;*.webp)|*.jpg;*.jpeg;*.png;*.webp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var savedPath = FileHelper.SaveStudentSignature(openFileDialog.FileName, Student.MemberCode);
                Student.SignaturePath = savedPath;
                OnPropertyChanged(nameof(Student));
            }
        }

        private void ExecuteSave()
        {
            if (string.IsNullOrWhiteSpace(Student.Name))
            {
                DialogHelper.ShowError("Please enter the student's full name.", "Validation Error");
                return;
            }

            if (_studentService.SaveStudent(Student, out var errorMessage))
            {
                DialogHelper.ShowInfo("Student record saved successfully!", "Success");
                OnSaved?.Invoke();
            }
            else
            {
                DialogHelper.ShowError(errorMessage, "Error Saving Student");
            }
        }
    }
}
