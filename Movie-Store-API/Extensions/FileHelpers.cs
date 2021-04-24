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
        private readonly string Folder = string.Empty;
        private readonly IWebHostEnvironment env;

        public FileHelpers(string folder, IWebHostEnvironment env)
        {
            Folder = folder;
            this.env = env;
        }

        public async Task<bool> UploadImage(IFormFile file)
        {
            string filePath = Path.Combine(env.ContentRootPath, Folder, file.FileName);
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
