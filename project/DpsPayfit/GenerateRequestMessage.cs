using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DpsPayfit
{
    /// <summary>
    /// Represents a Dps Generate-Request Message
    /// </summary>

    [XmlRoot(ElementName = "GenerateRequest", Namespace = "")]
    public class GenerateRequestMessage : IDpsMessage
    {
        public GenerateRequestMessage(){   }
        public GenerateRequestMessage(string pxPayUserId,
            string pxPayKey,
            string urlSuccess,
            string urlFail,
            Currency currencyInput,
            TxnType txnType,
            decimal amount = 0,
            ClientType clientType = global::DpsPayfit.ClientType.Internet,
            DateTimeOffset? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(pxPayUserId)) throw new ArgumentNullException(nameof(pxPayUserId));
            if (string.IsNullOrWhiteSpace(pxPayKey)) throw new ArgumentNullException(nameof(pxPayKey));
            if (string.IsNullOrWhiteSpace(urlSuccess)) throw new ArgumentNullException(nameof(urlSuccess));
            if (string.IsNullOrWhiteSpace(urlFail)) throw new ArgumentNullException(nameof(urlFail));

            PxPayUserId = pxPayUserId;
            PxPayKey = pxPayKey;
            UrlSuccess = urlSuccess;
            UrlFail = urlFail;
            Amount = amount;
            CurrencyInput = currencyInput;
            TxnType = txnType.ToString();
            ClientType = clientType.ToString();
            if (timeout.HasValue)
            {
                Timeout = $"{timeout:yyMMddHHmm}";
            }
        }

        [XmlElement]
        public string PxPayUserId { get; set; }

        [XmlElement]
        public string PxPayKey { get; set;}

        [XmlElement]
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
        public Currency CurrencyInput { get; set; }

        [XmlElement]
        public string TxnType { get; set;  }

        [XmlElement]
        public string UrlFail { get; set;  }

        [XmlElement]
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
        public string ClientType { get; set;  }
        
        [XmlElement()]
        public string Timeout { get; set; }
    }
}
