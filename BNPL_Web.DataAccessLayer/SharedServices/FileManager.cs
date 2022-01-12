using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccessLayer.SharedServices
{
   public class FileManager: IFileManager
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;
        public FileManager(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
        }
        public string Save(byte[] file, string extension,string PathName)
        {   
            var path = hostingEnvironment.ContentRootPath+ configuration.GetSection(PathName).Value;
            string str = Guid.NewGuid().ToString() + "." + extension;
            File.WriteAllBytes(path + str, file);
            return str;

        }

        public string Copy(string name,string PathName)
        {
            throw new NotImplementedException();
        }

        public void Delete(string name,string PathName)
        {
            var path = hostingEnvironment.ContentRootPath + configuration.GetSection(PathName).Value;
            File.Delete(path + name);
        }

        public byte[] Get(string name,string PathName)
        {
            var path = hostingEnvironment.ContentRootPath + configuration.GetSection(PathName).Value;
            return File.ReadAllBytes(path + name);
        }

        public bool Exists(string name,string PathName)
        {
            throw new NotImplementedException();
        }

        public string GetBase64Extension(string data)
        {
            var base64 = data.Substring(0, 5);

            switch (base64.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                default:
                    return string.Empty;
            }
        }

        public string Save(IFormFile file, string FileName, string PathName)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return this.Save(ms.ToArray(), Path.GetExtension(FileName), PathName);
            }
        }
    }
}
