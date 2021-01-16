using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TailorApp.Domain.Entities.Base
{
    public class ImageUploader
    {

        public string UploadImages(IFormFile ImageUpload, string applicationImagePath, string dbImagePath)
        {
            if (!Directory.Exists(applicationImagePath))
            {
                Directory.CreateDirectory(applicationImagePath);
            }
            if (ImageUpload.Length > 0)
            {
                string extension = Path.GetExtension(ImageUpload.FileName);
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {
                    string fileName = DateTime.Now.ToString("yymmssfff");
                    string path = Path.Combine(applicationImagePath, fileName) + extension;

                    string dbPath = Path.Combine(dbImagePath, fileName) + extension;
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        ImageUpload.CopyTo(stream);
                    }
                    return dbPath;
                }
            }
            return null;
        }
        public void DeleteImageDirectory(string path)
        {
            //string path = Path.Combine(_env.WebRootPath + imagePath);//wwwroot/Users/file.jpg
            try
            {
                if (File.Exists(path))
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                    File.Delete(path);
                }
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }

        }
    }
}
