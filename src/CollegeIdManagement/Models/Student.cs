using System;

namespace CollegeIdManagement.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string MemberCode { get; set; } = string.Empty;
        public string RollNo { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string MotherName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string AlternateMobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; } = "Male";
        public string Category { get; set; } = "General";
        public string BloodGroup { get; set; } = "O+";
        public string MaritalStatus { get; set; } = "Single";

        public string Session { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Semester { get; set; } = "Semester 1";
        public string Year { get; set; } = "1st Year";
        public string RegistrationNumber { get; set; } = string.Empty;
        public string EnrollmentNumber { get; set; } = string.Empty;
        public string AdmissionNumber { get; set; } = string.Empty;

        public string StudentType { get; set; } = "Regular";
        public StudentStatus Status { get; set; } = StudentStatus.Active;

        public DateTime? AdmissionDate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTill { get; set; }

        public string Address { get; set; } = string.Empty;
        public string Village { get; set; } = string.Empty;
        public string Post { get; set; } = string.Empty;
        public string PoliceStation { get; set; } = string.Empty;
        public string District { get; set; } = "Muzaffarpur";
        public string State { get; set; } = "Bihar";
        public string Country { get; set; } = "India";
        public string PinCode { get; set; } = string.Empty;

        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactMobile { get; set; } = string.Empty;

        public string PhotoPath { get; set; } = string.Empty;
        public string SignaturePath { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public bool IsIdGenerated { get; set; } = false;
        public DateTime? IdGeneratedAt { get; set; }
        public bool IsIdPrinted { get; set; } = false;
        public DateTime? IdPrintedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
