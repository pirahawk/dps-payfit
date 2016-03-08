using System;
using System.IO;
using System.Xml.Serialization;

namespace DpsPayfit.Client
{
    public static class XmlMessageSerializer
    {
        public static string SerializeToXml<TMessage>(TMessage requestMessage)
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
    }
}