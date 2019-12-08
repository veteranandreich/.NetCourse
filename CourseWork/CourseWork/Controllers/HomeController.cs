using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;

namespace CourseWork.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TextHandler(string text, string key, string EncryptOrDecrypt, string Download, string name)
        {
            if (text == null) text = "";
            if ((key == "") || (key == null)) key = "a";
            if (EncryptOrDecrypt == "Encrypt") text = VigenereEncryptor.Encrypt(text, key, 0, out int _);
            if (EncryptOrDecrypt == "Decrypt") text = VigenereEncryptor.Decrypt(text, key, 0, out int _);
            if (Download == "true")
            {
                if ((name == "") || (name == null))
                {
                    name = "solution.txt";
                }
                else if ((name.Length < 4) || (name.Substring(name.Length - 4) != ".txt")) name += ".txt";
                return File(Encoding.UTF8.GetBytes(text), "text/plain", name);
            }
            ViewBag.Text = text;
            return View();
        }
    }
}