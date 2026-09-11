using System;
using QRCoder;

namespace CollegeIdManagement.Services
{
    public class QrCodeService
    {
        public byte[] GenerateQrCodePngBytes(string text, int pixelsPerModule = 10)
        {
            if (string.IsNullOrWhiteSpace(text)) text = "SNSEC";
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(pixelsPerModule);
        }

        public string GenerateStudentQrPayload(string memberCode, string name, string course, string rollNo)
        {
            return $"SNSEC|MEMBER:{memberCode}|NAME:{name}|COURSE:{course}|ROLL:{rollNo}";
        }
    }
}
