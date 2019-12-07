using Microsoft.AspNetCore.Mvc;
using CourseWork.Controllers;
using Xunit;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace CourseWork.Tests
{
    public class FileControllerTests
    {
        FileController controller = new FileController();
        [Fact]
        public void IndexTest()
        {
            var result = controller.Index() as ViewResult;
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void FileToTxtHandlerTest()
        {
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Мама мыла раму")), 0, 100, "TestTxt", "TestTxt.txt" );
            var result = controller.FileHandler(file, "solution", "мама", "Decrypt", "true") as FileContentResult;
            byte[] bytes = result.FileContents;
            Assert.IsType<FileContentResult>(result);
            Assert.Equal("solution.txt", result.FileDownloadName);
            Assert.Equal(VigenereEncryptor.Decrypt("Мама мыла раму", "мама", 0, out int _), Encoding.UTF8.GetString(bytes));
            Assert.Equal(@"text/plain", result.ContentType);
        }
        [Fact]
        public void FileToTextHandlerTest()
        {
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Мама мыла раму")), 0, 100, "TestTxt", "TestTxt.txt");
            var result = controller.FileHandler(file, "123", "Кто мыл раму", "Encrypt", "") as ViewResult;
            Assert.IsType<ViewResult>(result);
            Assert.Equal(VigenereEncryptor.Encrypt("Мама мыла раму", "Кто мыл раму", 0, out int _), result?.ViewData["Text"]);
        }

        //public void DocxToDocxHandlerTest()
    }
}
