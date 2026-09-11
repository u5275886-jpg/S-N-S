using System;

namespace CollegeIdManagement.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty; // Login, Student Created, ID Printed, etc.
        public string Entity { get; set; } = string.Empty; // Student, Course, Settings, User
        public string Details { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
