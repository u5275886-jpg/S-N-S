using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ClosedXML.Excel;
using CollegeIdManagement.Models;

namespace CollegeIdManagement.Services
{
    public class ExcelService
    {
        public void ExportStudentsToExcel(List<Student> students, string filePath)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Students");

            worksheet.Cell(1, 1).Value = "Roll No";
            worksheet.Cell(1, 2).Value = "Member Code";
            worksheet.Cell(1, 3).Value = "Name";
            worksheet.Cell(1, 4).Value = "Father Name";
            worksheet.Cell(1, 5).Value = "Mother Name";
            worksheet.Cell(1, 6).Value = "Mobile";
            worksheet.Cell(1, 7).Value = "Email";
            worksheet.Cell(1, 8).Value = "Date of Birth";
            worksheet.Cell(1, 9).Value = "Gender";
            worksheet.Cell(1, 10).Value = "Category";
            worksheet.Cell(1, 11).Value = "Blood Group";
            worksheet.Cell(1, 12).Value = "Course";
            worksheet.Cell(1, 13).Value = "Session";
            worksheet.Cell(1, 14).Value = "Department";
            worksheet.Cell(1, 15).Value = "Semester";
            worksheet.Cell(1, 16).Value = "Registration No";
            worksheet.Cell(1, 17).Value = "Enrollment No";
            worksheet.Cell(1, 18).Value = "Address";
            worksheet.Cell(1, 19).Value = "District";
            worksheet.Cell(1, 20).Value = "State";
            worksheet.Cell(1, 21).Value = "Pin Code";
            worksheet.Cell(1, 22).Value = "Valid Till";

            int row = 2;
            foreach (var s in students)
            {
                worksheet.Cell(row, 1).Value = s.RollNo;
                worksheet.Cell(row, 2).Value = s.MemberCode;
                worksheet.Cell(row, 3).Value = s.Name;
                worksheet.Cell(row, 4).Value = s.FatherName;
                worksheet.Cell(row, 5).Value = s.MotherName;
                worksheet.Cell(row, 6).Value = s.Mobile;
                worksheet.Cell(row, 7).Value = s.Email;
                worksheet.Cell(row, 8).Value = s.DateOfBirth?.ToString("yyyy-MM-dd") ?? "";
                worksheet.Cell(row, 9).Value = s.Gender;
                worksheet.Cell(row, 10).Value = s.Category;
                worksheet.Cell(row, 11).Value = s.BloodGroup;
                worksheet.Cell(row, 12).Value = s.Course;
                worksheet.Cell(row, 13).Value = s.Session;
                worksheet.Cell(row, 14).Value = s.Department;
                worksheet.Cell(row, 15).Value = s.Semester;
                worksheet.Cell(row, 16).Value = s.RegistrationNumber;
                worksheet.Cell(row, 17).Value = s.EnrollmentNumber;
                worksheet.Cell(row, 18).Value = s.Address;
                worksheet.Cell(row, 19).Value = s.District;
                worksheet.Cell(row, 20).Value = s.State;
                worksheet.Cell(row, 21).Value = s.PinCode;
                worksheet.Cell(row, 22).Value = s.ValidTill?.ToString("yyyy-MM-dd") ?? "";
                row++;
            }

            workbook.SaveAs(filePath);
            AuditService.Log(AuthenticationService.CurrentUser?.Username ?? "Admin", "Excel Exported", "Student", $"Exported {students.Count} students to Excel: {filePath}");
        }

        public (int imported, int skipped, List<string> errors) ImportStudentsFromExcel(string filePath)
        {
            int imported = 0;
            int skipped = 0;
            var errors = new List<string>();

            if (!File.Exists(filePath))
            {
                errors.Add("Excel file not found.");
                return (0, 0, errors);
            }

            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
            {
                errors.Add("No worksheet found in Excel file.");
                return (0, 0, errors);
            }

            var studentService = new StudentService();
            var rollService = new RollNumberService();
            var codeService = new StudentCodeService();

            var rows = worksheet.RowsUsed().Skip(1); // Skip Header
            foreach (var row in rows)
            {
                try
                {
                    var name = row.Cell(3).GetString().Trim();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        skipped++;
                        continue;
                    }

                    var course = row.Cell(12).GetString().Trim();
                    var session = row.Cell(13).GetString().Trim();
                    if (string.IsNullOrWhiteSpace(course)) course = "B.A";
                    if (string.IsNullOrWhiteSpace(session)) session = "2026-27";

                    var memberCode = row.Cell(2).GetString().Trim();
                    if (string.IsNullOrWhiteSpace(memberCode))
                    {
                        memberCode = codeService.GenerateNextMemberCode(course, session);
                    }

                    var rollNo = row.Cell(1).GetString().Trim();
                    if (string.IsNullOrWhiteSpace(rollNo))
                    {
                        rollNo = rollService.GenerateNextRollNumber(session, course);
                    }

                    DateTime? dob = null;
                    if (DateTime.TryParse(row.Cell(8).GetString(), out var parsedDob)) dob = parsedDob;

                    DateTime? validTill = null;
                    if (DateTime.TryParse(row.Cell(22).GetString(), out var parsedValidTill)) validTill = parsedValidTill;

                    var student = new Student
                    {
                        RollNo = rollNo,
                        MemberCode = memberCode,
                        Name = name,
                        FatherName = row.Cell(4).GetString().Trim(),
                        MotherName = row.Cell(5).GetString().Trim(),
                        Mobile = row.Cell(6).GetString().Trim(),
                        Email = row.Cell(7).GetString().Trim(),
                        DateOfBirth = dob,
                        Gender = row.Cell(9).GetString().Trim(),
                        Category = row.Cell(10).GetString().Trim(),
                        BloodGroup = row.Cell(11).GetString().Trim(),
                        Course = course,
                        Session = session,
                        Department = row.Cell(14).GetString().Trim(),
                        Semester = row.Cell(15).GetString().Trim(),
                        RegistrationNumber = row.Cell(16).GetString().Trim(),
                        EnrollmentNumber = row.Cell(17).GetString().Trim(),
                        Address = row.Cell(18).GetString().Trim(),
                        District = row.Cell(19).GetString().Trim(),
                        State = row.Cell(20).GetString().Trim(),
                        PinCode = row.Cell(21).GetString().Trim(),
                        ValidTill = validTill,
                        Status = StudentStatus.Active
                    };

                    if (studentService.SaveStudent(student, out var err))
                    {
                        imported++;
                    }
                    else
                    {
                        skipped++;
                        errors.Add($"Row {row.RowNumber()}: {err}");
                    }
                }
                catch (Exception ex)
                {
                    skipped++;
                    errors.Add($"Row {row.RowNumber()}: {ex.Message}");
                }
            }

            AuditService.Log(AuthenticationService.CurrentUser?.Username ?? "Admin", "Excel Imported", "Student", $"Imported {imported} students, skipped {skipped}.");
            return (imported, skipped, errors);
        }
    }
}
