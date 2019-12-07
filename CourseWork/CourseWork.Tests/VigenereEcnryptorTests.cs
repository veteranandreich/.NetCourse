using Xunit;

namespace CourseWork.Tests
{
    public class VigenereEcnryptorTests
    {
        [Fact]
        public void VigenereDecryptTest()
        {
            string EncryptedText = "Оээ гфъбьукн ввщэшс, ъяяснямпдоп (12312?№*:!№*) унжцёывоисш ъцээф ьчура хърбъо";

            Assert.Equal("Это тестовая строка, проверяющая (12312?№*:!№*) дешифрующий метод моего класса", VigenereEncryptor.Decrypt(EncryptedText, "скорпион", 0, out int _));
        }
        
        [Fact]
        public void VigenereEncryptTest()
        {
            string DecryptedText = "Мама мыла раму !!11!";
            Assert.Equal("Щаща щыша эащу !!11!", VigenereEncryptor.Encrypt(DecryptedText, "мама", 0, out int _));
        }

    }
}
