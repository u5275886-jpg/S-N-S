using System.Windows;
using CollegeIdManagement.Models;
using CollegeIdManagement.Services;
using CollegeIdManagement.ViewModels;

namespace CollegeIdManagement.Views.IdCards
{
    public partial class IdCardPreviewView : Window
    {
        private readonly Student _student;
        private readonly PrintService _printService = new();

        public IdCardPreviewView(Student student)
        {
            InitializeComponent();
            _student = student;
            DataContext = new IdCardTemplateWrapper(student);
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            var userControl = new System.Windows.Controls.UserControl { Content = CardContentArea };
            if (_printService.PrintControl(userControl, $"Student_ID_{_student.RollNo}"))
            {
                var studentService = new StudentService();
                studentService.MarkIdPrinted(_student.Id);
                DialogHelper.ShowInfo("Printed successfully!", "Print Success");
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
