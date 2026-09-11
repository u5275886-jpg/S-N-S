using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CollegeIdManagement.Services
{
    public class PrintService
    {
        public bool PrintControl(UserControl visual, string documentName)
        {
            try
            {
                var printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(visual, documentName);
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Print failed: {ex.Message}", "Print Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            return false;
        }
    }
}
