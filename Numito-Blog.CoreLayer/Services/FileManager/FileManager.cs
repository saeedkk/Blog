using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Numito_Blog.CoreLayer.Services.FileManager
{
    public class FileManager : IFileManager
    {
        public string SaveFileAndReturnName(IFormFile file, string savePath)
        {
            if (file == null)
                throw new Exception("File is Null");
            
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),savePath.Replace("/","\\"));

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fullPath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(fullPath,FileMode.Create);

            file.CopyTo(stream);

            return fileName;
        }
    }
}