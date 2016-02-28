using Xunit;
using System.IO;
using System.Xml.Serialization;

namespace DpsPayfit.Test
{
    public class MessageSerializationFactoryTest
    {
        [Fact]
        public void CanDeserializeThings()
        {
            var obj = new MyTestType
            {
                FirstName = "Foo",
                LastName = "Bar"
            };
            
            using (var stream = new MemoryStream())
            {
                XmlSerializer s = new XmlSerializer(typeof(MyTestType));

                var writer = new StreamWriter(stream) as TextWriter;

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                s.Serialize(writer, obj, ns);
                stream.Position = 0;


                var textReader = new StreamReader(stream) as TextReader;
                var xml = textReader.ReadToEnd();
                Assert.True(xml.Contains("FirstName"));
                Assert.True(xml.Contains("LastName"));
                Assert.False(xml.Contains("SomethingElse"));
            }
        }
    }


    public class MyTestType
    {
        [XmlElement]
        public string FirstName { get; set; }
        [XmlElement]
        public string LastName { get; set; }

        //[XmlIgnore]
        public string SomethingElse { get; set; }

        private int SecretCode { get; set; }
    }
}