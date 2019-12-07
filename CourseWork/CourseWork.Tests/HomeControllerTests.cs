using Microsoft.AspNetCore.Mvc;
using CourseWork.Controllers;
using Xunit;
using System.Text;

namespace CourseWork.Tests
{
    public class HomeControllerTests
    {
        HomeController controller = new HomeController();
        [Fact]
        public void IndexTest()
        {
            var result = controller.Index() as ViewResult;
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void TextToTextHandlerTest()
        {
            var result = controller.TextHandler("Мама мыла раму", "мама", "Encrypt", "", "") as ViewResult;
            Assert.IsType<ViewResult>(result);
            Assert.Equal(VigenereEncryptor.Encrypt("Мама мыла раму", "мама", 0, out int _) ,result?.ViewData["Text"]);
        }
        [Fact]
        public void TextToTxtHandlerTest()
        {
            var result = controller.TextHandler("Мама мыла раму", "мама", "Encrypt", "true", "solution.txt") as FileContentResult;
            byte[] bytes = result.FileContents;
            Assert.IsType<FileContentResult>(result);
            Assert.Equal(VigenereEncryptor.Encrypt("Мама мыла раму", "мама", 0, out int _), Encoding.UTF8.GetString(bytes));
            Assert.Equal(@"text/plain", result.ContentType);
        }
    }
}
