using CollegeIdManagement.Helpers;
using CollegeIdManagement.Models;
using CollegeIdManagement.Services;

namespace CollegeIdManagement.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private readonly CollegeSettingsService _settingsService = new();
        private readonly BackupService _backupService = new();

        public CollegeSettings Settings { get; set; }

        public RelayCommand SaveSettingsCommand { get; }
        public RelayCommand UploadLogoCommand { get; }
        public RelayCommand BackupNowCommand { get; }
        public RelayCommand RestoreBackupCommand { get; }

        public SettingsViewModel()
        {
            Settings = _settingsService.GetSettings();

            SaveSettingsCommand = new RelayCommand(ExecuteSaveSettings);
            UploadLogoCommand = new RelayCommand(ExecuteUploadLogo);
            BackupNowCommand = new RelayCommand(ExecuteBackupNow);
            RestoreBackupCommand = new RelayCommand(ExecuteRestoreBackup);
        }

        private void ExecuteUploadLogo()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                Settings.CollegeLogoPath = openFileDialog.FileName;
                OnPropertyChanged(nameof(Settings));
            }
        }

        private void ExecuteSaveSettings()
        {
            _settingsService.SaveSettings(Settings);
            DialogHelper.ShowInfo("College profile settings saved successfully!", "Settings Saved");
        }

        private void ExecuteBackupNow()
        {
            var path = _backupService.CreateBackupZip(string.Empty);
            DialogHelper.ShowInfo($"Database and assets backup created successfully!\nPath: {path}", "Backup Complete");
        }

        private void ExecuteRestoreBackup()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Zip Backup Files (*.zip)|*.zip"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (DialogHelper.Confirm("Are you sure you want to restore this backup? Current data will be overwritten.", "Confirm Restore"))
                {
                    if (_backupService.RestoreBackupZip(openFileDialog.FileName, out var err))
                    {
                        DialogHelper.ShowInfo("Backup restored successfully!", "Restore Complete");
                    }
                    else
                    {
                        DialogHelper.ShowError($"Failed to restore backup: {err}", "Restore Failed");
                    }
                }
            }
        }
    }
}
