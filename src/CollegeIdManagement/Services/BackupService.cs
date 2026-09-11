using System;
using System.IO;
using System.IO.Compression;
using CollegeIdManagement.Helpers;

namespace CollegeIdManagement.Services
{
    public class BackupService
    {
        public string CreateBackupZip(string targetDirectory)
        {
            FileHelper.EnsureDirectories();
            if (string.IsNullOrWhiteSpace(targetDirectory))
            {
                targetDirectory = FileHelper.BackupsDirectory;
            }

            var timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            var zipFileName = $"CollegeID_Backup_{timestamp}.zip";
            var zipFilePath = Path.Combine(targetDirectory, zipFileName);

            using (var zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                // Add DB
                var dbFile = Path.Combine(FileHelper.DatabaseDirectory, "college.db");
                if (File.Exists(dbFile))
                {
                    zip.CreateEntryFromFile(dbFile, "Database/college.db");
                }

                // Add Photos
                if (Directory.Exists(FileHelper.PhotosDirectory))
                {
                    foreach (var file in Directory.GetFiles(FileHelper.PhotosDirectory))
                    {
                        zip.CreateEntryFromFile(file, Path.Combine("Photos", Path.GetFileName(file)));
                    }
                }

                // Add Signatures
                if (Directory.Exists(FileHelper.SignaturesDirectory))
                {
                    foreach (var file in Directory.GetFiles(FileHelper.SignaturesDirectory))
                    {
                        zip.CreateEntryFromFile(file, Path.Combine("Signatures", Path.GetFileName(file)));
                    }
                }
            }

            AuditService.Log(AuthenticationService.CurrentUser?.Username ?? "Admin", "Backup Created", "Backup", $"Created backup archive: {zipFileName}");
            return zipFilePath;
        }

        public bool RestoreBackupZip(string zipFilePath, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                if (!File.Exists(zipFilePath))
                {
                    errorMessage = "Backup file not found.";
                    return false;
                }

                using (var zip = ZipFile.OpenRead(zipFilePath))
                {
                    foreach (var entry in zip.Entries)
                    {
                        var targetPath = Path.Combine(FileHelper.AppDataRoot, entry.FullName);
                        var targetDir = Path.GetDirectoryName(targetPath);
                        if (!string.IsNullOrEmpty(targetDir) && !Directory.Exists(targetDir))
                        {
                            Directory.CreateDirectory(targetDir);
                        }

                        if (!string.IsNullOrEmpty(entry.Name))
                        {
                            entry.ExtractToFile(targetPath, true);
                        }
                    }
                }

                AuditService.Log(AuthenticationService.CurrentUser?.Username ?? "Admin", "Backup Restored", "Backup", $"Restored backup from: {zipFilePath}");
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
