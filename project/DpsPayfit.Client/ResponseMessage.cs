using System.Xml.Serialization;

namespace DpsPayfit.Client
{
    [XmlRoot(ElementName = "Response", Namespace = "")]
    public class ResponseMessage
    {
        [XmlAttribute(AttributeName = "valid")]
        public int ValidFlag { get; set; }

        [XmlElement(ElementName = "AmountSettlement")]
        public decimal AmountSettlement { get; set; }

        [XmlElement(ElementName = "AuthCode")]
        public string AuthCode { get; set; }

        [XmlElement(ElementName = "CardName")]
        public string CardName { get; set; }

        [XmlElement(ElementName = "CardNumber")]
        public string CardNumber { get; set; }

        [XmlElement(ElementName = "DateExpiry")]
        public string DateExpiry { get; set; }

        [XmlElement(ElementName = "DpsTxnRef")]
        public string DpsTxnRef { get; set; }

        [XmlElement(ElementName = "Success")]
        public Success Success { get; set; }

        [XmlElement(ElementName = "ResponseText")]
        public string ResponseText { get; set; }

        [XmlElement(ElementName = "DpsBillingId")]
        public string DpsBillingId { get; set; }

        [XmlElement(ElementName = "CardHolderName")]
        public string CardHolderName { get; set; }

        [XmlElement(ElementName = "CurrencySettlement")]
        public Currency CurrencySettlement { get; set; }

        [XmlElement(ElementName = "TxnData1")]
        public string TxnData1 { get; set; }

        [XmlElement(ElementName = "TxnData2")]
        public string TxnData2 { get; set; }

        [XmlElement(ElementName = "TxnData3")]
        public string TxnData3 { get; set; }

        [XmlElement(ElementName = "TxnType")]
        public TxnType TxnType { get; set; }

        [XmlElement(ElementName = "CurrencyInput")]
        public Currency CurrencyInput { get; set; }

        [XmlElement(ElementName = "MerchantReference")]
        public string MerchantReference { get; set; }

        [XmlElement(ElementName = "ClientInfo")]
        public string ClientInfo { get; set; }

        [XmlElement(ElementName = "TxnId")]
        public string TxnId { get; set; }

        [XmlElement(ElementName = "EmailAddress")]
        public string EmailAddress { get; set; }

        [XmlElement(ElementName = "BillingId")]
        public string BillingId { get; set; }

        [XmlElement(ElementName = "TxnMac")]
        public string TxnMac { get; set; }

        [XmlElement(ElementName = "CardNumber2")]
        public string CardNumber2 { get; set; }

        [XmlElement(ElementName = "DateSettlement")]
        public string DateSettlement { get; set; }

        [XmlElement(ElementName = "IssuerCountryId")]
        public string IssuerCountryId { get; set; }

        [XmlElement(ElementName = "Cvc2ResultCode")]
        public string Cvc2ResultCode { get; set; }

        [XmlElement(ElementName = "ReCo")]
        public string ReCo { get; set; }

        [XmlElement(ElementName = "ProductSku")]
        public string ProductSku { get; set; }

        [XmlElement(ElementName = "ShippingName")]
        public string ShippingName { get; set; }

        [XmlElement(ElementName = "ShippingAddress")]
        public string ShippingAddress { get; set; }

        [XmlElement(ElementName = "ShippingPostalCode")]
        public string ShippingPostalCode { get; set; }

        [XmlElement(ElementName = "ShippingPhoneNumber")]
        public string ShippingPhoneNumber { get; set; }

        [XmlElement(ElementName = "ShippingMethod")]
        public string ShippingMethod { get; set; }

        [XmlElement(ElementName = "BillingName")]
        public string BillingName { get; set; }

        [XmlElement(ElementName = "BillingPostalCode")]
        public string BillingPostalCode { get; set; }

        [XmlElement(ElementName = "BillingAddress")]
        public string BillingAddress { get; set; }

        [XmlElement(ElementName = "BillingPhoneNumber")]
        public string BillingPhoneNumber { get; set; }

        [XmlElement(ElementName = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [XmlElement(ElementName = "AccountInfo")]
        public string AccountInfo { get; set; }

        [XmlIgnore]
        public bool IsValid
        {
            get
            {
                return ValidFlag == 1 && ReCo == "00";
            }
        }
    }
}
