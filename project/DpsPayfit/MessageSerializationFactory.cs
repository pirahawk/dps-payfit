using System;
using System.Threading.Tasks;
using DpsPayfit.Validation;
using System.IO;
using System.Xml.Serialization;

namespace DpsPayfit
{
    public interface IMessageSerializationFactory
    {
        Task<string> SerializeToXmlAsync<TMessage>(TMessage requestMessage);
    }

    public class MessageSerializationFactory : IMessageSerializationFactory
    {
        public async Task<string> SerializeToXmlAsync<TMessage>(TMessage requestMessage)
        {
            if (requestMessage == null) throw new ArgumentNullException(nameof(requestMessage));
            using (var stream = new MemoryStream())
            {
                XmlSerializer s = new XmlSerializer(requestMessage.GetType());
                var writer = new StreamWriter(stream) as TextWriter;
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);
                s.Serialize(writer, requestMessage, ns);
                stream.Position = 0;

                var textReader = new StreamReader(stream) as TextReader;
                return await Task.FromResult(textReader.ReadToEnd());
            }
        }
    }
}