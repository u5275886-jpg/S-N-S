using CollegeIdManagement.Models;

namespace CollegeIdManagement.Services
{
    public class IdCardService
    {
        private readonly QrCodeService _qrCodeService = new();
        private readonly CollegeSettingsService _settingsService = new();

        public byte[] GetStudentQrCodeBytes(Student student)
        {
            var payload = _qrCodeService.GenerateStudentQrPayload(student.MemberCode, student.Name, student.Course, student.RollNo);
            return _qrCodeService.GenerateQrCodePngBytes(payload, 8);
        }

        public CollegeSettings GetCollegeSettings()
        {
            return _settingsService.GetSettings();
        }
    }
}
