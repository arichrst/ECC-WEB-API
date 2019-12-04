using System.IO;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ECC.Controllers;
using System.Threading.Tasks;

namespace ECC.Services
{
    public class FileServices
    {
        MasterController Controller;
        public FileServices(MasterController controller)
        {
            Controller = controller;
        }
        public async Task Upload(IFormFile file, string filename , string path)
        {
            string filePath = Controller.HttpContext.Request.Path + "/" + path + "/";
            Console.WriteLine("UPLOAD : " + file.FileName + "\nto Directory : " + filePath + filename);
            if(!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            using (var fileStream = new FileStream(filePath + filename, FileMode.Create)) {
                await file.CopyToAsync(fileStream);
            }
        }

        
    }
}