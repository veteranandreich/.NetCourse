using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWork.Controllers
{
    public class FileController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileHandler(IFormFile file, string name, string key, string EncryptOrDecrypt, string download)
        {
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                string ResponseText = "";
                Console.WriteLine(download);
                if (extension == ".txt")
                {
                    string text = "";
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        text = reader.ReadToEnd();
                    }
                    if (EncryptOrDecrypt == "Encrypt") ResponseText = VigenereEncryptor.Encrypt(text, key, 0, out int _);
                    if (EncryptOrDecrypt == "Decrypt") ResponseText = VigenereEncryptor.Decrypt(text, key, 0, out int _);
                    if ((name.Length < 4) || (name.Substring(name.Length - 4) != ".txt")) name += ".txt";
                    if (download == "true") return File(Encoding.UTF8.GetBytes(ResponseText), "text/plain", name);
                    ViewBag.Text = ResponseText;
                    return View();
                }
                if (extension == ".docx")
                {
                    byte[] BytesResponse;
                    using (Stream stream = file.OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            stream.CopyTo(ms);
                            var doc = new WordDocument(ms);
                            if (EncryptOrDecrypt == "Encrypt") doc.EncryptDecrypt(key, true);
                            if (EncryptOrDecrypt == "Decrypt") doc.EncryptDecrypt(key, false);
                            doc.Dispose();
                            BytesResponse = ms.ToArray();
                        }
                        if ((name.Length<5)||(name.Substring(name.Length - 5) != ".docx")) name += ".docx";
                        return File(BytesResponse, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", name);
                    }
                }
            }
            return RedirectToAction("Index", "File");
        }
    }
}