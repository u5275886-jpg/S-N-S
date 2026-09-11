using System;

namespace CollegeIdManagement.Models
{
    public class IdCardTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // e.g. Default Student Front, Staff Back
        public string TargetType { get; set; } = "Student"; // Student / Staff
        public string Side { get; set; } = "Front"; // Front / Back
        public string PrimaryColorHex { get; set; } = "#751A38"; // Maroon
        public string SecondaryColorHex { get; set; } = "#B89135"; // Gold
        public string BackgroundColorHex { get; set; } = "#FFFFFF";
        public string TemplateJson { get; set; } = string.Empty; // Visual designer layout elements JSON
        public bool IsDefault { get; set; } = false;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
