using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DpsPayfit.Client
{
    public static class XmlMessageSerializer
    {
        public static string Serialize<TMessage>(TMessage requestMessage)
        {
            if (requestMessage == null) throw new ArgumentNullException(nameof(requestMessage));
            using (var stream = new MemoryStream())
            {
                XmlSerializer s = new XmlSerializer(requestMessage.GetType());
                var writer = new StreamWriter(stream) as TextWriter;

                //need to do this to remove unnecessary ns schemas from getting generated
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                s.Serialize(writer, requestMessage, ns);

                stream.Position = 0;
                var textReader = new StreamReader(stream) as TextReader;
                return textReader.ReadToEnd();
            }
        }

        public static TObject Deserialize<TObject>(string xmlBody)
        {
            var xmlSerializer = new XmlSerializer(typeof(TObject));
            var xmlReader = XmlReader.Create(new StringReader(xmlBody));
            return (TObject) xmlSerializer.Deserialize(xmlReader);
        }
    }
}