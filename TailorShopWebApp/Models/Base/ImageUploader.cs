using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TailorManagementApp.Models.Base
{
    public class ImageUploader
    {
      
        public void CreateDirectory(string galleryPath)
        {
            if (!Directory.Exists(galleryPath))
            {
                Directory.CreateDirectory(galleryPath);
            }
        }
        public string UploadImages(IFormFile ImageUpload,string applicationImagePath,string dbImagePath)
        {
            if (ImageUpload.Length > 0)
            {
                var extension = Path.GetExtension(ImageUpload.FileName);
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {
                    var fileName = DateTime.Now.ToString("yymmssfff");
                    var path = Path.Combine(applicationImagePath, fileName) + extension;

                    var dbPath = Path.Combine(dbImagePath, fileName) + extension;
                    using (var stream = new FileStream(path, FileMode.Create))
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
