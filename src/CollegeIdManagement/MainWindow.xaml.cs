using System.Windows;
using CollegeIdManagement.ViewModels;

namespace CollegeIdManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
