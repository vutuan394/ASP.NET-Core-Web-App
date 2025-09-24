using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Web_QLNT.Functions
{
    public class ImageHelper
    {
        private readonly IWebHostEnvironment _environment;

        public ImageHelper(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, string fileNamePrefix = "")
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return string.Empty;
            }

            try
            {
                // Tạo tên file duy nhất
                var extension = Path.GetExtension(imageFile.FileName);
                var fileName = $"{fileNamePrefix}_{Guid.NewGuid()}{extension}";
                var imagesPath = Path.Combine(_environment.WebRootPath, "images");

                // Đảm bảo thư mục tồn tại
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var filePath = Path.Combine(imagesPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                return fileName;
            }
            catch
            {
                return string.Empty;
            }
        }

        public void DeleteImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            try
            {
                var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch
            {
                // Log lỗi nếu cần
            }
        }

        public static bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            return allowedExtensions.Contains(extension);
        }
    }
}