using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DpsPayfit.Client
{
    /// <summary>
    /// Represents a Dps Generate-Request Message
    /// </summary>

    [XmlRoot(ElementName = "GenerateRequest", Namespace = "")]
    public class GenerateRequestMessage : IDpsMessage
    {
        public GenerateRequestMessage(){   }
        public GenerateRequestMessage(DateTimeOffset? timeout = null)
        {
            if (timeout.HasValue)
            {
                Timeout = $"{timeout:yyMMddHHmm}";
            }
        }

        [XmlElement]
        [Required]
        public string PxPayUserId { get; set; }

        [XmlElement]
        [Required]
        public string PxPayKey { get; set;}

        [XmlElement]
        [Required]
        public string AmountInput
        {
            get
            {
                return CurrencyInput == Currency.JPY ? $"{Amount:F0}" : $"{Amount:F2}";
            }
        }

        [XmlIgnore]
        [Range(0, 999999.99)]
        public decimal Amount { get; set; }

        [XmlElement]
        [Required]
        public Currency CurrencyInput { get; set; }

        [XmlElement]
        [Required]
        public TxnType TxnType { get; set;  }

        [XmlElement]
        [Required]
        public string UrlFail { get; set;  }

        [XmlElement]
        [Required]
        public string UrlSuccess { get; set;  }
        
        [XmlElement]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [XmlElement]
        [Range(0, 1)]
        public int? EnableAddBillCard { get; set; }

        [XmlElement]
        public string MerchantReference { get; set; }

        [XmlElement]
        public string DpsBillingId { get; set; }
        
        [XmlElement]
        public string TxnData1 { get; set; }
        
        [XmlElement]
        public string TxnData2 { get; set; }
        
        [XmlElement]
        public string TxnData3 { get; set; }
        
        [XmlElement]
        public string TxnId { get; set; }
        
        [XmlElement]
        public string Opt { get; set; }
        
        [XmlElement]
        public ClientType ClientType { get; set;  }
        
        [XmlElement()]
        public string Timeout { get; set; }
    }
}
