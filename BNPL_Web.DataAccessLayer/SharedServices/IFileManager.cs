using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DataAccessLayer.SharedServices
{
    public interface IFileManager
    {
        string Save(byte[] file, string extension, string PathName);

        string Copy(string name,string PathName);

        void Delete(string name,string PathName);

        byte[] Get(string name ,string PathName);

        bool Exists(string name,string PathName);

        string GetBase64Extension(string data);
        string Save(IFormFile file, string FileName, string PathName);
    }
}
