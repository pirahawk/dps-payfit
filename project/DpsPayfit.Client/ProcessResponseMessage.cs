using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DpsPayfit.Client
{
    [XmlRoot(ElementName = "ProcessResponse", Namespace = "")]
    public class ProcessResponseMessage
    {
        public ProcessResponseMessage(){}

        [XmlElement]
        [Required]
        public string PxPayUserId { get; set; }

        [XmlElement]
        [Required]
        public string PxPayKey { get; set; }

        [XmlElement]
        [Required]
        public string Response { get; set; }
    }
}
