using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_API.Extensions
{
    public class FileHelpers
    {
        private readonly string Folder = "Static";
        private readonly IWebHostEnvironment env;

        public FileHelpers(string folder, IWebHostEnvironment env)
        {
            Folder = folder;
            this.env = env;
        }

        public async Task<bool> UploadImage(IFormFile file)
        {
            string filePath = Path.Combine(env.ContentRootPath, Folder, file.FileName);
            Console.WriteLine(filePath);
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteImage(string fileName)
        {
            string filePath = Path.Combine(env.ContentRootPath, Folder, fileName);
            try
            {
                if (File.Exists(filePath))
                {
                    var fileInfo = new FileInfo(filePath);
                    fileInfo.Delete();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
