using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_store.Extensions
{
    public class UploadFileHelper
    {
        private static UploadFileHelper _instance;

        private UploadFileHelper()
        {

        }

        public static UploadFileHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UploadFileHelper();
                return _instance;
            }
            private set => _instance = value;
        }

        public async Task Upload(IFormFile file, IWebHostEnvironment env)
        {
            string filePath = Path.Combine(env.WebRootPath, "img", file.FileName);
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(string filename, IWebHostEnvironment env)
        {
            var getFile = new FileInfo(Path.Combine(env.WebRootPath, "img", filename));
            try
            {
                getFile.Delete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
