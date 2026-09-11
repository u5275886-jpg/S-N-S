using System;
using System.Linq;
using CollegeIdManagement.Data;

namespace CollegeIdManagement.Services
{
    public class StudentCodeService
    {
        public string GenerateNextMemberCode(string courseCode, string sessionName)
        {
            var collegePrefix = "SNSEC";
            var courseClean = string.IsNullOrWhiteSpace(courseCode) ? "GEN" : courseCode.Replace(".", "").Trim().ToUpper();

            // Extract start year from session e.g., "2026-27" -> "2026"
            var yearStr = DateTime.Now.Year.ToString();
            if (!string.IsNullOrWhiteSpace(sessionName))
            {
                var parts = sessionName.Split(new[] { '-', '/' });
                if (parts.Length > 0 && parts[0].Trim().Length >= 4)
                {
                    yearStr = parts[0].Trim().Substring(0, 4);
                }
            }

            var prefix = $"{collegePrefix}/{courseClean}/{yearStr}/";

            using var context = new AppDbContext();
            var existingCodes = context.Students
                .Where(s => s.MemberCode.StartsWith(prefix))
                .Select(s => s.MemberCode)
                .ToList();

            int maxSeq = 0;
            foreach (var code in existingCodes)
            {
                var lastPart = code.Substring(prefix.Length);
                if (int.TryParse(lastPart, out int seq))
                {
                    if (seq > maxSeq) maxSeq = seq;
                }
            }

            int nextSeq = maxSeq + 1;
            return $"{prefix}{nextSeq:D3}";
        }
    }
}
