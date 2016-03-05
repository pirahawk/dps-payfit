using Xunit;
using System.IO;
using System.Xml.Serialization;

namespace DpsPayfit.Test
{
    public class XmlXmlMessageSerializerTest
    {
        [Fact]
        public void SerializesToXmlAsExpected()
        {
            var toSerialize = new MyTestType
            {
                FirstName = "Foo",
                LastName = "Bar"
            };
            var serializer = new XmlXmlMessageSerializerFixture().Build();
            var xml = serializer.SerializeToXml(toSerialize);
            Assert.True(xml.Contains("FirstName"));
            Assert.True(xml.Contains("LastName"));
            Assert.False(xml.Contains("SomethingElse"));
            Assert.False(xml.Contains("SecretCode"));
        }

        [Fact]
        public void CanSerializeGenerateRequestMessage()
        {
            var generateRequestMessage = new GenerateRequestMessageFixture
            {

            }.Build();

            var serializer = new XmlXmlMessageSerializerFixture().Build();
            var xml = serializer.SerializeToXml(generateRequestMessage);
            //TODO
            //Assert.True(xml.Contains("PxPayUserId"));
            //Assert.True(xml.Contains("PxPayKey"));
            //Assert.True(xml.Contains("AmountInput"));
            //Assert.True(xml.Contains("CurrencyInput"));
            //Assert.True(xml.Contains("TxnType"));
            //Assert.True(xml.Contains("UrlFail"));
            //Assert.True(xml.Contains("UrlSuccess"));
            //Assert.True(xml.Contains("EmailAddress"));
            //Assert.True(xml.Contains("EnableAddBillCard"));
            //Assert.True(xml.Contains("MerchantReference"));
            //Assert.True(xml.Contains("DpsBillingId"));

            //Assert.True(xml.Contains("TxnData1"));
            //Assert.True(xml.Contains("TxnData2"));
            //Assert.True(xml.Contains("TxnData3"));
            //Assert.True(xml.Contains("TxnId"));
            //Assert.True(xml.Contains("Opt"));
            //Assert.True(xml.Contains("ClientType"));
            //Assert.True(xml.Contains("Timeout"));
        }
    }

    public class XmlXmlMessageSerializerFixture
    {
        public XmlXmlMessageSerializer Build()
        {
            return new XmlXmlMessageSerializer();
        }
    }

    [XmlRoot(ElementName = "MyTestType", Namespace = "")]
    public class MyTestType
    {
        [XmlElement]
        public string FirstName { get; set; }
        [XmlElement]
        public string LastName { get; set; }

        [XmlIgnore]
        public string SomethingElse { get; set; }

        private int SecretCode { get; set; }
    }
}