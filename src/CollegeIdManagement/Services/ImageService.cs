using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace CollegeIdManagement.Services
{
    public class ImageService
    {
        public bool CropAndResizeImage(string sourcePath, string destinationPath, int targetWidth, int targetHeight)
        {
            try
            {
                if (!File.Exists(sourcePath)) return false;

                using var image = Image.Load(sourcePath);
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(targetWidth, targetHeight),
                    Mode = ResizeMode.Crop
                }));

                var destDir = Path.GetDirectoryName(destinationPath);
                if (!string.IsNullOrEmpty(destDir) && !Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }

                image.Save(destinationPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
