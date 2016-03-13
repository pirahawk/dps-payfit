using System.Xml.Serialization;

namespace DpsPayfit.Client
{
    [XmlRoot(ElementName = "Request", Namespace = "")]
    public class RequestMessage
    {
        [XmlAttribute(AttributeName = "valid")]
        public int ValidFlag { get; set; }

        [XmlElement(ElementName = "URI")]
        public string Uri { get; set; }

        [XmlElement(ElementName = "Reco")]
        public string ResponseCode { get; set; }

        [XmlElement(ElementName = "ResponseText")]
        public string ResponseText { get; set; }

        [XmlIgnore]
        public bool IsValid
        {
            get
            {
                return ValidFlag == 1
                && string.IsNullOrWhiteSpace(ResponseCode)
                && string.IsNullOrWhiteSpace(ResponseText);
            }
        }
    }
}
