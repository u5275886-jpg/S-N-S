using CollegeIdManagement.Data;
using CollegeIdManagement.Services;
using Xunit;

namespace CollegeIdManagement.Tests
{
    public class ServiceTests
    {
        [Fact]
        public void PasswordHash_ReturnsValidHash()
        {
            var hash1 = DbSeeder.HashPassword("admin123");
            var hash2 = DbSeeder.HashPassword("admin123");

            Assert.NotNull(hash1);
            Assert.NotEmpty(hash1);
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void StudentCodeService_GeneratesCorrectFormat()
        {
            var service = new StudentCodeService();
            var code = service.GenerateNextMemberCode("B.A", "2026-27");

            Assert.NotNull(code);
            Assert.StartsWith("SNSEC/BA/2026/", code);
        }

        [Fact]
        public void RollNumberService_GeneratesNextRoll()
        {
            var service = new RollNumberService();
            var roll = service.GenerateNextRollNumber("2026-27", "B.A");

            Assert.NotNull(roll);
            Assert.Equal(3, roll.Length);
        }

        [Fact]
        public void QrCodeService_GeneratesPngBytes()
        {
            var qrService = new QrCodeService();
            var payload = qrService.GenerateStudentQrPayload("SNSEC/BA/2026/001", "RAHUL KUMAR", "B.A", "001");
            var bytes = qrService.GenerateQrCodePngBytes(payload);

            Assert.NotNull(bytes);
            Assert.True(bytes.Length > 0);
        }
    }
}
