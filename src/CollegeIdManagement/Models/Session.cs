using System;

namespace CollegeIdManagement.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // e.g. 2026-27 or 2026-2029
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddYears(1);
        public bool IsCurrent { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
