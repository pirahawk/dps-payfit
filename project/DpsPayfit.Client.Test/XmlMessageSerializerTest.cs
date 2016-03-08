using Xunit;
using System.IO;
using System.Xml.Serialization;

namespace DpsPayfit.Client.Test
{
    public class XmlMessageSerializerTest
    {
        [Fact]
        public void SerializesToXmlAsExpected()
        {
            var toSerialize = new MyTestType
            {
                FirstName = "Foo",
                LastName = "Bar"
            };
            var xml = XmlMessageSerializer.SerializeToXml(toSerialize);
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

            var xml = XmlMessageSerializer.SerializeToXml(generateRequestMessage);
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