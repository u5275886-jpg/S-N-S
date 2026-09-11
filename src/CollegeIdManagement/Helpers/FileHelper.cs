using System;
using System.IO;

namespace CollegeIdManagement.Helpers
{
    public static class FileHelper
    {
        public static string AppDataRoot => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CollegeIdManagement");

        public static string DatabaseDirectory => Path.Combine(AppDataRoot, "Database");
        public static string PhotosDirectory => Path.Combine(AppDataRoot, "Photos");
        public static string SignaturesDirectory => Path.Combine(AppDataRoot, "Signatures");
        public static string BackupsDirectory => Path.Combine(AppDataRoot, "Backups");
        public static string ExportsDirectory => Path.Combine(AppDataRoot, "Exports");
        public static string LogsDirectory => Path.Combine(AppDataRoot, "Logs");

        public static void EnsureDirectories()
        {
            Directory.CreateDirectory(AppDataRoot);
            Directory.CreateDirectory(DatabaseDirectory);
            Directory.CreateDirectory(PhotosDirectory);
            Directory.CreateDirectory(SignaturesDirectory);
            Directory.CreateDirectory(BackupsDirectory);
            Directory.CreateDirectory(ExportsDirectory);
            Directory.CreateDirectory(LogsDirectory);
        }

        public static string SaveStudentPhoto(string sourceFilePath, string memberCode)
        {
            EnsureDirectories();
            if (!File.Exists(sourceFilePath)) return string.Empty;

            var ext = Path.GetExtension(sourceFilePath);
            var safeCode = memberCode.Replace("/", "_").Replace("\\", "_");
            var targetFileName = $"photo_{safeCode}_{Guid.NewGuid().ToString().Substring(0, 8)}{ext}";
            var targetPath = Path.Combine(PhotosDirectory, targetFileName);

            File.Copy(sourceFilePath, targetPath, true);
            return targetPath;
        }

        public static string SaveStudentSignature(string sourceFilePath, string memberCode)
        {
            EnsureDirectories();
            if (!File.Exists(sourceFilePath)) return string.Empty;

            var ext = Path.GetExtension(sourceFilePath);
            var safeCode = memberCode.Replace("/", "_").Replace("\\", "_");
            var targetFileName = $"sig_{safeCode}_{Guid.NewGuid().ToString().Substring(0, 8)}{ext}";
            var targetPath = Path.Combine(SignaturesDirectory, targetFileName);

            File.Copy(sourceFilePath, targetPath, true);
            return targetPath;
        }
    }
}
