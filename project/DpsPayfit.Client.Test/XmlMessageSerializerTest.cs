using Xunit;
using System.IO;
using System.Xml.Serialization;

namespace DpsPayfit.Client.Test
{
    public class XmlMessageSerializerTest
    {
        [Fact]
        public void SerializesToXmlAsExpected()
        {
            var toSerialize = new MyTestType
            {
                FirstName = "Foo",
                LastName = "Bar"
            };
            var xml = XmlMessageSerializer.Serialize(toSerialize);
            Assert.True(xml.Contains("FirstName"));
            Assert.True(xml.Contains("LastName"));
            Assert.False(xml.Contains("SomethingElse"));
            Assert.False(xml.Contains("SecretCode"));
        }

        [Fact]
        public void CanSerializeGenerateRequestMessage()
        {
            var generateRequestMessage = new GenerateRequestMessageFixture().Build();
            var xml = XmlMessageSerializer.Serialize(generateRequestMessage);
        }

        [Fact]
        public void CanSerializeProcessResponseMessage()
        {
            var responseMessage = new ProcessResponseMessageFixture().Build();
            var xml = XmlMessageSerializer.Serialize(responseMessage);
        }

        [Fact]
        public void CanDeserializeRequestMessage() {

            const string message = @"<Request valid=""1"">
                    <URI>https://test.com/123</URI>
                </Request>";

            var requestMessage = XmlMessageSerializer.Deserialize<RequestMessage>(message);
            Assert.Equal("https://test.com/123", requestMessage.Uri);
            Assert.True(requestMessage.IsValid);
        }

        [Fact]
        public void CanDeserializeProcessResponseMessage() {
            const string message = @"<Response valid='1'>
                    < AmountSettlement > 1.00 </ AmountSettlement >
                    < AuthCode > 200315 </ AuthCode >
                    < CardName > Visa </ CardName >
                    < CardNumber > 411111........11 </ CardNumber >
                    < DateExpiry > 0121 </ DateExpiry >
                    < DpsTxnRef > 000000031a69f0ab </ DpsTxnRef >
                    < Success > 1 </ Success >
                    < ResponseText > APPROVED </ ResponseText >
                    < DpsBillingId ></ DpsBillingId >
                    < CardHolderName > SADFSDF </ CardHolderName >
                    < CurrencySettlement > NZD </ CurrencySettlement >
                    < TxnData1 > Data 1 </ TxnData1 >
                    < TxnData2 > Data 2 </ TxnData2 >
                    < TxnData3 > Data 3 </ TxnData3 >
                    < TxnType > Auth </ TxnType >
                    < CurrencyInput > NZD </ CurrencyInput >
                    < MerchantReference > My Reference </ MerchantReference >
                    < ClientInfo > 122.57.157.23 </ ClientInfo >
                    < TxnId ></ TxnId >
                    < EmailAddress ></ EmailAddress >
                    < BillingId ></ BillingId >
                    < TxnMac > 2BC20210 </ TxnMac >
                    < CardNumber2 ></ CardNumber2 >
                    < DateSettlement > 20160313 </ DateSettlement >
                    < IssuerCountryId > 0 </ IssuerCountryId >
                    < Cvc2ResultCode > NotUsed </ Cvc2ResultCode >
                    < ReCo > 00 </ ReCo >
                    < ProductSku ></ ProductSku >
                    < ShippingName ></ ShippingName >
                    < ShippingAddress ></ ShippingAddress >
                    < ShippingPostalCode ></ ShippingPostalCode >
                    < ShippingPhoneNumber ></ ShippingPhoneNumber >
                    < ShippingMethod ></ ShippingMethod >
                    < BillingName ></ BillingName >
                    < BillingPostalCode ></ BillingPostalCode >
                    < BillingAddress ></ BillingAddress >
                    < BillingPhoneNumber ></ BillingPhoneNumber >
                    < PhoneNumber ></ PhoneNumber >
                    < AccountInfo ></ AccountInfo >
                </ Response > ";

            var responseMessage = XmlMessageSerializer.Deserialize<ResponseMessage>(message);
            Assert.Equal("000000031a69f0ab", responseMessage.DpsTxnRef);
            Assert.True(responseMessage.IsValid);
        }
    }

    [XmlRoot(ElementName = "MyTestType", Namespace = "")]
    public class MyTestType
    {
        [XmlElement]
        public string FirstName { get; set; }
        [XmlElement]
        public string LastName { get; set; }

        [XmlIgnore]
        public string SomethingElse { get; set; }

        private int SecretCode { get; set; }
    }
}