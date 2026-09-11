using System.Linq;
using System.Windows;
using CollegeIdManagement.Data;

namespace CollegeIdManagement.Views.IdCards
{
    public partial class StudentVerificationWindow : Window
    {
        public StudentVerificationWindow()
        {
            InitializeComponent();
        }

        private void BtnVerify_Click(object sender, RoutedEventArgs e)
        {
            var token = TxtQrToken.Text.Trim();
            if (string.IsNullOrWhiteSpace(token))
            {
                MessageBox.Show("Please enter or scan a QR payload token.");
                return;
            }

            using var context = new AppDbContext();
            var student = context.Students.FirstOrDefault(s =>
                s.MemberCode.ToLower() == token.ToLower() ||
                token.Contains($"MEMBER:{s.MemberCode}") ||
                s.RollNo.ToLower() == token.ToLower());

            if (student != null)
            {
                TxtStudentName.Text = student.Name;
                TxtMemberCode.Text = $"Member Code: {student.MemberCode}";
                TxtCourseSession.Text = $"Course: {student.Course} | Session: {student.Session}";
                ResultCard.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("No matching student record found for this QR token.", "Verification Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
