using System;

namespace CollegeIdManagement.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; // e.g. BA, BSC, BCOM, MA
        public string Name { get; set; } = string.Empty; // e.g. Bachelor of Arts
        public string Department { get; set; } = string.Empty;
        public int DurationYears { get; set; } = 3;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
