using System.Collections.ObjectModel;
using System.Linq;
using CollegeIdManagement.Data;
using CollegeIdManagement.Helpers;
using CollegeIdManagement.Models;

namespace CollegeIdManagement.ViewModels
{
    public class DesignerElement : ObservableObject
    {
        public string Type { get; set; } = "Text"; // Text, Photo, Logo, QR
        public string Content { get; set; } = string.Empty;
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; } = 100;
        public double Height { get; set; } = 25;
        public int FontSize { get; set; } = 10;
        public string FontColorHex { get; set; } = "#1F2024";
    }

    public class TemplateDesignerViewModel : ObservableObject
    {
        public ObservableCollection<DesignerElement> CanvasElements { get; } = new();

        public RelayCommand AddTextTagCommand { get; }
        public RelayCommand SaveTemplateCommand { get; }

        public TemplateDesignerViewModel()
        {
            AddTextTagCommand = new RelayCommand(p => ExecuteAddTag(p?.ToString()));
            SaveTemplateCommand = new RelayCommand(ExecuteSaveTemplate);

            // Default elements layout preview
            CanvasElements.Add(new DesignerElement { Type = "Text", Content = "{{college_name}}", X = 20, Y = 10, FontSize = 12 });
            CanvasElements.Add(new DesignerElement { Type = "Text", Content = "NAME: {{student_name}}", X = 110, Y = 60, FontSize = 10 });
            CanvasElements.Add(new DesignerElement { Type = "Text", Content = "ROLL NO: {{roll_no}}", X = 110, Y = 85, FontSize = 10 });
            CanvasElements.Add(new DesignerElement { Type = "Text", Content = "MEMBER CODE: {{member_code}}", X = 110, Y = 110, FontSize = 10 });
        }

        private void ExecuteAddTag(string? tag)
        {
            if (string.IsNullOrWhiteSpace(tag)) return;
            CanvasElements.Add(new DesignerElement { Type = "Text", Content = tag, X = 110, Y = 130 });
        }

        private void ExecuteSaveTemplate()
        {
            using var context = new AppDbContext();
            var template = context.IdCardTemplates.FirstOrDefault(t => t.IsDefault);
            if (template != null)
            {
                template.UpdatedAt = System.DateTime.Now;
                context.SaveChanges();
            }
            DialogHelper.ShowInfo("ID Card Design Template saved successfully to database!", "Template Saved");
        }
    }
}
