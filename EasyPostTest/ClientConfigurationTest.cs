using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest
{
    [TestClass]
    public class ClientConfigurationTest {

        [TestMethod]
        public void TestApiKeyConstructor() {
            var config = new ClientConfiguration("someApiKey");

            Assert.AreEqual("someApiKey", config.ApiKey);
            Assert.AreEqual(ClientConfiguration.DefaultBaseUrl, config.ApiBase);
        }

        [TestMethod]
        public void TestApiKeyPlusBaseUrlConstructor() {
            var config = new ClientConfiguration("someApiKey", "http://foobar.com");

            Assert.AreEqual("someApiKey", config.ApiKey);
            Assert.AreEqual("http://foobar.com", config.ApiBase);
        }
    }
}
