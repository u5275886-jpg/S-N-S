using System;
using System.Collections.Generic;
using System.IO;
using CollegeIdManagement.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CollegeIdManagement.Services
{
    public class PdfService
    {
        private readonly IdCardService _idCardService = new();

        static PdfService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public void GenerateSingleStudentIdPdf(Student student, string outputPdfPath)
        {
            GenerateBulkStudentsIdPdf(new List<Student> { student }, outputPdfPath);
        }

        public void GenerateBulkStudentsIdPdf(List<Student> students, string outputPdfPath)
        {
            var settings = _idCardService.GetCollegeSettings();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(10, Unit.Millimeter);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9).FontFamily("Arial"));

                    page.Content().Grid(grid =>
                    {
                        grid.Columns(2); // 2 cards per row on A4

                        foreach (var student in students)
                        {
                            var qrBytes = _idCardService.GetStudentQrCodeBytes(student);

                            grid.Item().Padding(5, Unit.Millimeter).Width(85.60f, Unit.Millimeter).Height(53.98f, Unit.Millimeter).Border(1, Unit.Point).BorderColor(Colors.Grey.Medium).Column(col =>
                            {
                                // Header Maroon Band
                                col.Item().Background("#751A38").Padding(3).Row(row =>
                                {
                                    row.RelativeItem().Column(header =>
                                    {
                                        header.Item().Text(settings.CollegeName).FontSize(8).Bold().FontColor(Colors.White);
                                        header.Item().Text(settings.Address).FontSize(6).FontColor("#B89135");
                                        header.Item().Text("STUDENT IDENTITY CARD").FontSize(6).Bold().FontColor(Colors.White);
                                    });
                                });

                                // Body Content
                                col.Item().Padding(3).Row(row =>
                                {
                                    // Student Photo
                                    row.ConstantItem(22, Unit.Millimeter).Column(pcol =>
                                    {
                                        if (!string.IsNullOrEmpty(student.PhotoPath) && File.Exists(student.PhotoPath))
                                        {
                                            pcol.Item().Image(student.PhotoPath, ImageScaling.FitArea);
                                        }
                                        else
                                        {
                                            pcol.Item().Height(28).Background(Colors.Grey.Lighten2).Center().Text("PHOTO").FontSize(6);
                                        }

                                        pcol.Item().PaddingTop(2).Image(qrBytes, ImageScaling.FitArea);
                                    });

                                    // Details
                                    row.RelativeItem().PaddingLeft(5).Column(dcol =>
                                    {
                                        dcol.Item().Text($"Name: {student.Name}").Bold().FontSize(7);
                                        dcol.Item().Text($"Father: {student.FatherName}").FontSize(6);
                                        dcol.Item().Text($"Roll No: {student.RollNo} | Member Code: {student.MemberCode}").FontSize(6);
                                        dcol.Item().Text($"Course: {student.Course} | Session: {student.Session}").FontSize(6);
                                        dcol.Item().Text($"DOB: {student.DateOfBirth?.ToString("dd/MM/yyyy") ?? "-"} | Blood: {student.BloodGroup}").FontSize(6);
                                        dcol.Item().Text($"Valid Till: {student.ValidTill?.ToString("dd/MM/yyyy") ?? "-"}").FontSize(6).Bold();
                                    });
                                });
                            });
                        }
                    });
                });
            }).GeneratePdf(outputPdfPath);
        }
    }
}
