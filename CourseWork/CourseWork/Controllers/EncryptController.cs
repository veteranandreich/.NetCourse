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
    public class EncryptController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EncryptFile(IFormFile file, string name, string key)
        {
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (extension == ".txt")
                {
                    string text = "";
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        text = reader.ReadToEnd();
                    }
                    string EncryptedText =  VigenereEncryptor.Encrypt(text, key, 0, out int _);
                    return File(Encoding.UTF8.GetBytes(EncryptedText), "text/plain", name);
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
                            doc.Encrypt(key);
                            doc.Save();
                            doc.Dispose();
                            BytesResponse = ms.ToArray();
                        }
                        return File(BytesResponse, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", name);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}