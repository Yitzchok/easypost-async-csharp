using System.Collections.Generic;
using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace EasyPostTest
{
    [TestClass]
    public class JsonNetWithRootElementSerializerTests
    {
        [TestMethod]
        public void DeserializeWithArray()
        {
            var content = "[{\"object\":\"CarrierType\",\"type\":\"Account\",\"readable\":\"Account\",\"logo\":null,\"fields\":{\"credentials\":{\"access_key_id\":{\"visibility\":\"visible\",\"label\":\"Access Key\"},\"secret_key\":{\"visibility\":\"password\",\"label\":\"Secret Key\"},\"merchant_id\":{\"visibility\":\"visible\",\"label\":\"ID\"}}}}]";

            var deserialize = Deserialize<IList<CarrierType>>(null, content);

            Assert.AreEqual(1, deserialize.Count);
        }

        [TestMethod]
        public void DeserializeWithoutRootElementSet()
        {
            var content = "{\"id\":\"adr_029b9cbb384c4adeb6c4d53a62feb21a\",\"object\":\"Address\",\"created_at\":\"2020-10-15T22:39:39+00:00\",\"updated_at\":\"2020-10-15T22:39:39+00:00\",\"name\":null,\"company\":\"Simpler Postage Inc\",\"street1\":\"164 Townsend Street\",\"street2\":\"Unit 1\",\"city\":\"San Francisco\",\"state\":\"CA\",\"zip\":\"94107\",\"country\":\"US\",\"phone\":null,\"email\":null,\"mode\":\"test\",\"carrier_facility\":null,\"residential\":null,\"federal_tax_id\":null,\"state_tax_id\":null,\"verifications\":{}}";

            var address = Deserialize<Address>(null, content);

            Assert.AreEqual("adr_029b9cbb384c4adeb6c4d53a62feb21a", address.Id);
        }

        [TestMethod]
        public void DeserializeWithoutRootElement()
        {
            var content = "{\"id\":\"adr_029b9cbb384c4adeb6c4d53a62feb21a\",\"object\":\"Address\",\"created_at\":\"2020-10-15T22:39:39+00:00\",\"updated_at\":\"2020-10-15T22:39:39+00:00\",\"name\":null,\"company\":\"Simpler Postage Inc\",\"street1\":\"164 Townsend Street\",\"street2\":\"Unit 1\",\"city\":\"San Francisco\",\"state\":\"CA\",\"zip\":\"94107\",\"country\":\"US\",\"phone\":null,\"email\":null,\"mode\":\"test\",\"carrier_facility\":null,\"residential\":null,\"federal_tax_id\":null,\"state_tax_id\":null,\"verifications\":{}}";

            var address = Deserialize<Address>("address", content);

            Assert.AreEqual("adr_029b9cbb384c4adeb6c4d53a62feb21a", address.Id);
        }

        [TestMethod]
        public void DeserializeWithRootElement()
        {
            var content = "{\"address\":{\"id\":\"adr_e208ec77896740008aff58ec3647606a\",\"object\":\"Address\",\"created_at\":\"2020-10-15T22:41:05+00:00\",\"updated_at\":\"2020-10-15T22:41:05+00:00\",\"name\":null,\"company\":\"SIMPLER POSTAGE INC\",\"street1\":\"164 TOWNSEND ST UNIT 1\",\"street2\":\"\",\"city\":\"SAN FRANCISCO\",\"state\":\"CA\",\"zip\":\"94107-1990\",\"country\":\"US\",\"phone\":null,\"email\":null,\"mode\":\"test\",\"carrier_facility\":null,\"residential\":false,\"federal_tax_id\":null,\"state_tax_id\":null,\"verifications\":{\"zip4\":{\"success\":true,\"errors\":[],\"details\":null},\"delivery\":{\"success\":true,\"errors\":[],\"details\":{\"latitude\":37.77893,\"longitude\":-122.39279,\"time_zone\":\"America/Los_Angeles\"}}}}}";

            var address = Deserialize<Address>("address", content);
            Assert.AreEqual("adr_e208ec77896740008aff58ec3647606a", address.Id);
        }

        private T Deserialize<T>(string rootElement, string content)
        {
            var serializer = new JsonNetWithRootElementSerializer { RootElement = rootElement };

            var response = new RestResponse
            {
                Content = content,
            };

            return serializer.Deserialize<T>(response);
        }
    }
}