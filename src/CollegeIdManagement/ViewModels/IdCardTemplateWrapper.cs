using CollegeIdManagement.Helpers;
using CollegeIdManagement.Models;
using CollegeIdManagement.Services;

namespace CollegeIdManagement.ViewModels
{
    public class IdCardTemplateWrapper : ObservableObject
    {
        public Student Student { get; set; }
        public CollegeSettings CollegeSettings { get; set; }
        public byte[] QrCodeImage { get; set; }

        public IdCardTemplateWrapper(Student student)
        {
            Student = student;
            var idCardService = new IdCardService();
            CollegeSettings = idCardService.GetCollegeSettings();
            QrCodeImage = idCardService.GetStudentQrCodeBytes(student);
        }
    }
}
