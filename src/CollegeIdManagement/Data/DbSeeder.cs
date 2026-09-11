using System.Security.Cryptography;
using System.Text;
using CollegeIdManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeIdManagement.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Ensure settings
            if (!context.Settings.Any())
            {
                context.Settings.Add(new CollegeSettings
                {
                    Id = 1,
                    CollegeName = "SHYAM NANDAN SAHAY COLLEGE",
                    ShortName = "SNSEC",
                    AffiliationText = "Permanent Affiliated Unit of B.R. Ambedkar Bihar University",
                    Address = "MUZAFFARPUR, BIHAR",
                    District = "MUZAFFARPUR",
                    State = "BIHAR",
                    PinCode = "842001",
                    Phone = "+91 9128559609",
                    Email = "info@snscollege.ac.in",
                    Website = "www.snscollege.ac.in",
                    PrincipalName = "Dr. Principal",
                    DirectorName = "Director",
                    IdCardInstructions = "1. This ID card is non-transferable.\n2. Display card at all times inside campus.\n3. Return if found to Administrative Office."
                });
            }

            // Ensure Admin user (admin / admin123)
            if (!context.Users.Any(u => u.Username == "admin"))
            {
                context.Users.Add(new User
                {
                    Username = "admin",
                    PasswordHash = HashPassword("admin123"),
                    FullName = "System Administrator",
                    Email = "admin@snscollege.ac.in",
                    Role = UserRole.Admin,
                    IsActive = true
                });
            }

            // Ensure Departments
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department { Code = "ARTS", Name = "Faculty of Arts", HeadOfDepartment = "Dr. A. K. Sharma" },
                    new Department { Code = "SCI", Name = "Faculty of Science", HeadOfDepartment = "Dr. R. N. Singh" },
                    new Department { Code = "COMM", Name = "Faculty of Commerce", HeadOfDepartment = "Dr. S. K. Verma" }
                );
            }

            // Ensure Courses
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(
                    new Course { Code = "B.A", Name = "Bachelor of Arts", Department = "Faculty of Arts", DurationYears = 3 },
                    new Course { Code = "B.Sc", Name = "Bachelor of Science", Department = "Faculty of Science", DurationYears = 3 },
                    new Course { Code = "B.Com", Name = "Bachelor of Commerce", Department = "Faculty of Commerce", DurationYears = 3 },
                    new Course { Code = "M.A", Name = "Master of Arts", Department = "Faculty of Arts", DurationYears = 2 }
                );
            }

            // Ensure Sessions
            if (!context.Sessions.Any())
            {
                context.Sessions.AddRange(
                    new Session { Name = "2023-24", IsCurrent = false, IsActive = true },
                    new Session { Name = "2024-25", IsCurrent = false, IsActive = true },
                    new Session { Name = "2025-26", IsCurrent = false, IsActive = true },
                    new Session { Name = "2026-27", IsCurrent = true, IsActive = true }
                );
            }

            // Ensure Sample Demo Students (clearly marked demo)
            if (!context.Students.Any())
            {
                context.Students.AddRange(
                    new Student
                    {
                        MemberCode = "SNSEC/BA/2026/001",
                        RollNo = "001",
                        Name = "RAHUL KUMAR (DEMO)",
                        FatherName = "RAMESH PRASAD",
                        MotherName = "SUNITA DEVI",
                        Mobile = "9876543210",
                        Email = "rahul.demo@example.com",
                        DateOfBirth = new DateTime(2005, 5, 15),
                        Gender = "Male",
                        Category = "General",
                        BloodGroup = "B+",
                        Session = "2026-27",
                        Course = "B.A",
                        Department = "Faculty of Arts",
                        Semester = "Semester 1",
                        Year = "1st Year",
                        RegistrationNumber = "REG20260001",
                        EnrollmentNumber = "ENR20260001",
                        AdmissionNumber = "ADM20260001",
                        StudentType = "Regular",
                        Status = StudentStatus.Active,
                        AdmissionDate = new DateTime(2026, 7, 1),
                        ValidFrom = new DateTime(2026, 7, 1),
                        ValidTill = new DateTime(2029, 6, 30),
                        Address = "Station Road, Muzaffarpur, Bihar",
                        District = "Muzaffarpur",
                        State = "Bihar",
                        PinCode = "842001",
                        EmergencyContactName = "RAMESH PRASAD",
                        EmergencyContactMobile = "9876543211",
                        Notes = "DEMO SAMPLE STUDENT RECORD"
                    },
                    new Student
                    {
                        MemberCode = "2023-27/BA/01",
                        RollNo = "01",
                        Name = "ARTI DEVI (DEMO)",
                        FatherName = "AJAY KUMAR JHA",
                        MotherName = "PINKI DEVI",
                        Mobile = "9128559609",
                        Email = "arti.demo@example.com",
                        DateOfBirth = new DateTime(2005, 8, 20),
                        Gender = "Female",
                        Category = "General",
                        BloodGroup = "A+",
                        Session = "2023-24",
                        Course = "B.A",
                        Department = "Faculty of Arts",
                        Semester = "Semester 1",
                        Year = "1st Year",
                        RegistrationNumber = "REG20230001",
                        EnrollmentNumber = "ENR20230001",
                        AdmissionNumber = "ADM20230001",
                        StudentType = "Regular",
                        Status = StudentStatus.Active,
                        AdmissionDate = new DateTime(2023, 10, 5),
                        ValidFrom = new DateTime(2023, 10, 5),
                        ValidTill = new DateTime(2027, 10, 5),
                        Address = "TITRA, BISHANPUR, SAKRA, MUZAFFARPUR, BIHAR",
                        District = "MUZAFFARPUR",
                        State = "BIHAR",
                        PinCode = "843105",
                        EmergencyContactName = "AJAY KUMAR JHA",
                        EmergencyContactMobile = "9128559609",
                        Notes = "DEMO SAMPLE STUDENT RECORD FROM PREVIOUS YEAR"
                    }
                );
            }

            // Ensure Templates
            if (!context.IdCardTemplates.Any())
            {
                context.IdCardTemplates.AddRange(
                    new IdCardTemplate
                    {
                        Name = "Standard Horizontal Student ID Front",
                        TargetType = "Student",
                        Side = "Front",
                        PrimaryColorHex = "#751A38",
                        SecondaryColorHex = "#B89135",
                        BackgroundColorHex = "#FFFFFF",
                        IsDefault = true
                    },
                    new IdCardTemplate
                    {
                        Name = "Standard Horizontal Student ID Back",
                        TargetType = "Student",
                        Side = "Back",
                        PrimaryColorHex = "#751A38",
                        SecondaryColorHex = "#B89135",
                        BackgroundColorHex = "#FFFFFF",
                        IsDefault = true
                    }
                );
            }

            context.SaveChanges();
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
