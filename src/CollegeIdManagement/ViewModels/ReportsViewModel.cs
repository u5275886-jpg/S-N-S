using System;
using System.IO;
using System.Linq;
using CollegeIdManagement.Data;
using CollegeIdManagement.Helpers;

namespace CollegeIdManagement.ViewModels
{
    public class ReportsViewModel : ObservableObject
    {
        public System.Collections.ObjectModel.ObservableCollection<Models.AuditLog> AuditLogs { get; } = new();

        public RelayCommand RefreshLogsCommand { get; }
        public RelayCommand ExportLogsCommand { get; }

        public ReportsViewModel()
        {
            RefreshLogsCommand = new RelayCommand(LoadLogs);
            ExportLogsCommand = new RelayCommand(ExecuteExportLogs);

            LoadLogs();
        }

        private void LoadLogs()
        {
            AuditLogs.Clear();
            using var context = new AppDbContext();
            var logs = context.AuditLogs.OrderByDescending(a => a.Timestamp).Take(100).ToList();
            foreach (var l in logs) AuditLogs.Add(l);
        }

        private void ExecuteExportLogs()
        {
            FileHelper.EnsureDirectories();
            var path = Path.Combine(FileHelper.ExportsDirectory, $"AuditLogs_Export_{DateTime.Now:yyyyMMdd_HHmmss}.csv");

            var lines = AuditLogs.Select(l => $"\"{l.Timestamp:yyyy-MM-dd HH:mm:ss}\",\"{l.Username}\",\"{l.Action}\",\"{l.Entity}\",\"{l.Details}\"");
            var header = "\"Timestamp\",\"User\",\"Action\",\"Entity\",\"Details\"";
            File.WriteAllLines(path, new[] { header }.Concat(lines));

            DialogHelper.ShowInfo($"Audit logs exported successfully!\nFile: {path}", "Export Complete");
        }
    }
}
