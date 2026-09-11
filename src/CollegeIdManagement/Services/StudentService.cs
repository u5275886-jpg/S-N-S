using System;
using System.Collections.Generic;
using System.Linq;
using CollegeIdManagement.Data;
using CollegeIdManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeIdManagement.Services
{
    public class StudentService
    {
        public List<Student> GetAllStudents()
        {
            using var context = new AppDbContext();
            return context.Students.OrderByDescending(s => s.Id).ToList();
        }

        public Student? GetStudentById(int id)
        {
            using var context = new AppDbContext();
            return context.Students.FirstOrDefault(s => s.Id == id);
        }

        public List<Student> FilterStudents(string? searchTerm, string? session, string? course, string? department, StudentStatus? status)
        {
            using var context = new AppDbContext();
            var query = context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim().ToLower();
                query = query.Where(s => s.Name.ToLower().Contains(term) ||
                                         s.MemberCode.ToLower().Contains(term) ||
                                         s.RollNo.ToLower().Contains(term) ||
                                         s.Mobile.Contains(term) ||
                                         s.FatherName.ToLower().Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(session) && session != "All Sessions")
            {
                query = query.Where(s => s.Session == session);
            }

            if (!string.IsNullOrWhiteSpace(course) && course != "All Courses")
            {
                query = query.Where(s => s.Course == course);
            }

            if (!string.IsNullOrWhiteSpace(department) && department != "All Departments")
            {
                query = query.Where(s => s.Department == department);
            }

            if (status.HasValue)
            {
                query = query.Where(s => s.Status == status.Value);
            }

            return query.OrderByDescending(s => s.Id).ToList();
        }

        public bool SaveStudent(Student student, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(student.Name))
            {
                errorMessage = "Student name is required.";
                return false;
            }

            using var context = new AppDbContext();

            if (student.Id == 0)
            {
                student.CreatedAt = DateTime.Now;
                student.UpdatedAt = DateTime.Now;
                context.Students.Add(student);
                context.SaveChanges();

                AuditService.Log(AuthenticationService.CurrentUser?.Username ?? "Admin", "Student Created", "Student", $"Added student '{student.Name}' with code '{student.MemberCode}'.");
            }
            else
            {
                var existing = context.Students.FirstOrDefault(s => s.Id == student.Id);
                if (existing == null)
                {
                    errorMessage = "Student record not found.";
                    return false;
                }

                existing.MemberCode = student.MemberCode;
                existing.RollNo = student.RollNo;
                existing.Name = student.Name;
                existing.FatherName = student.FatherName;
                existing.MotherName = student.MotherName;
                existing.Mobile = student.Mobile;
                existing.AlternateMobile = student.AlternateMobile;
                existing.Email = student.Email;
                existing.Phone = student.Phone;
                existing.DateOfBirth = student.DateOfBirth;
                existing.Gender = student.Gender;
                existing.Category = student.Category;
                existing.BloodGroup = student.BloodGroup;
                existing.MaritalStatus = student.MaritalStatus;
                existing.Session = student.Session;
                existing.Course = student.Course;
                existing.Department = student.Department;
                existing.Semester = student.Semester;
                existing.Year = student.Year;
                existing.RegistrationNumber = student.RegistrationNumber;
                existing.EnrollmentNumber = student.EnrollmentNumber;
                existing.AdmissionNumber = student.AdmissionNumber;
                existing.StudentType = student.StudentType;
                existing.Status = student.Status;
                existing.AdmissionDate = student.AdmissionDate;
                existing.JoiningDate = student.JoiningDate;
                existing.ValidFrom = student.ValidFrom;
                existing.ValidTill = student.ValidTill;
                existing.Address = student.Address;
                existing.Village = student.Village;
                existing.Post = student.Post;
                existing.PoliceStation = student.PoliceStation;
                existing.District = student.District;
                existing.State = student.State;
                existing.Country = student.Country;
                existing.PinCode = student.PinCode;
                existing.EmergencyContactName = student.EmergencyContactName;
                existing.EmergencyContactMobile = student.EmergencyContactMobile;
                existing.PhotoPath = student.PhotoPath;
                existing.SignaturePath = student.SignaturePath;
                existing.Notes = student.Notes;
                existing.UpdatedAt = DateTime.Now;

                context.SaveChanges();
                AuditService.Log(AuthenticationService.CurrentUser?.Username ?? "Admin", "Student Updated", "Student", $"Updated student '{student.Name}' (ID: {student.Id}).");
            }

            return true;
        }

        public bool DeleteStudent(int id)
        {
            using var context = new AppDbContext();
            var student = context.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
                AuditService.Log(AuthenticationService.CurrentUser?.Username ?? "Admin", "Student Deleted", "Student", $"Deleted student '{student.Name}' (Code: {student.MemberCode}).");
                return true;
            }
            return false;
        }

        public void MarkIdGenerated(int studentId)
        {
            using var context = new AppDbContext();
            var student = context.Students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                student.IsIdGenerated = true;
                student.IdGeneratedAt = DateTime.Now;
                context.SaveChanges();
                AuditService.Log(AuthenticationService.CurrentUser?.Username ?? "Admin", "ID Generated", "Student", $"Generated ID for student '{student.Name}' ({student.MemberCode}).");
            }
        }

        public void MarkIdPrinted(int studentId)
        {
            using var context = new AppDbContext();
            var student = context.Students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                student.IsIdPrinted = true;
                student.IdPrintedAt = DateTime.Now;
                context.SaveChanges();
                AuditService.Log(AuthenticationService.CurrentUser?.Username ?? "Admin", "ID Printed", "Student", $"Printed ID for student '{student.Name}' ({student.MemberCode}).");
            }
        }
    }
}
