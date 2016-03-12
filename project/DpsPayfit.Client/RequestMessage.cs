using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DpsPayfit.Client
{
    [XmlRoot(ElementName = "Request", Namespace = "")]
    public class RequestMessage
    {
        [XmlAttribute(AttributeName = "valid")]
        public int ValidFlag { get; set; }

        [XmlIgnore]
        public bool IsValid { get { return ValidFlag == 1; } }

        [XmlElement(ElementName = "URI")]
        public string Uri { get; set; }
    }
}
