using System;
using System.Linq;
using CollegeIdManagement.Data;

namespace CollegeIdManagement.Services
{
    public class RollNumberService
    {
        public string GenerateNextRollNumber(string session, string course)
        {
            using var context = new AppDbContext();

            var existingRolls = context.Students
                .Where(s => s.Session == session && s.Course == course)
                .Select(s => s.RollNo)
                .ToList();

            int maxRoll = 0;
            foreach (var roll in existingRolls)
            {
                if (int.TryParse(roll, out int val))
                {
                    if (val > maxRoll) maxRoll = val;
                }
            }

            int nextRoll = maxRoll + 1;
            return $"{nextRoll:D3}";
        }

        public bool IsRollNumberTaken(string session, string course, string rollNo, int excludeStudentId = 0)
        {
            if (string.IsNullOrWhiteSpace(rollNo)) return false;

            using var context = new AppDbContext();
            return context.Students.Any(s => s.Session == session &&
                                             s.Course == course &&
                                             s.RollNo.ToLower() == rollNo.Trim().ToLower() &&
                                             s.Id != excludeStudentId);
        }
    }
}
