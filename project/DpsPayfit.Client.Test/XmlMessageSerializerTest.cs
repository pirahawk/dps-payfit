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
            var xml = XmlMessageSerializer.Serialize(toSerialize);
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

            var xml = XmlMessageSerializer.Serialize(generateRequestMessage);
        }

        [Fact]
        public void CanDeserialzieXml() {

            const string message = @"<Request valid=""1"">
                    <URI>https://test.com/123</URI>
                </Request>";

            var requestMessage = XmlMessageSerializer.Deserialize<RequestMessage>(message);
            Assert.Equal("https://test.com/123", requestMessage.Uri);
            Assert.True(requestMessage.IsValid);
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