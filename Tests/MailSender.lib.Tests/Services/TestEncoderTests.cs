using MailSender.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailSender.lib.Tests.Services
{
    [TestClass]
    public class TestEncoderTests
    {
        [TestMethod]
        public void Encode_ABC_return_BCD_with_key_1()
        {
            // A-A-A

            // Arrange - подготовка данных
            var str = "ABC";
            var Key = 1;
            var expected_result = "BCD";

            // Act - действие, нацеленное на тестирование кода
            var actual_result = TextEncoder.Encode(str, Key);

            // Assert - проверка утверждений
            Assert.AreEqual(expected_result, actual_result);

            //StringAssert.Matches();
            //CollectionAssert.AreEquivalent();
        }

        [TestMethod]
        public void Decode_BCD_return_ABC_with_key_1()
        {
            // A-A-A

            // Arrange - подготовка данных
            var str = "BCD";
            var Key = 1;
            var expected_result = "ABC";

            // Act - действие, нацеленное на тестирование кода
            var actual_result = TextEncoder.Decode(str, Key);

            // Assert - проверка утверждений
            Assert.AreEqual(expected_result, actual_result);

            //StringAssert.Matches();
            //CollectionAssert.AreEquivalent();
        }
    }
}
