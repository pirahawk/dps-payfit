using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xunit;

namespace DpsPayfit.Client.Test
{
    public class TestRefit
    {
        [Fact]
        public async Task RefitoWrks()
        {
            const string message = @"<GenerateRequest>
                <PxPayUserId>UserId</PxPayUserId>
                <PxPayKey>Key</PxPayKey>
                <MerchantReference>My Reference</MerchantReference>
                <TxnType>Purchase</TxnType>
                <AmountInput>1.00</AmountInput>
                <CurrencyInput>NZD</CurrencyInput>
                <TxnData1>Data 1</TxnData1>
                <TxnData2>Data 2</TxnData2>
                <TxnData3>Data 3</TxnData3>
                <EmailAddress></EmailAddress>
                <TxnId></TxnId>
                <UrlSuccess>https://www.dpsdemo.com/SandboxSuccess.aspx</UrlSuccess>
                <UrlFail>https://www.dpsdemo.com/SandboxSuccess.aspx</UrlFail>
                </GenerateRequest>
                ";


            var service = RestService.For<IPaymentExpressApi>(@"https://sec.paymentexpress.com");

            var response = await service.GenerateRequest(message);
            var msg = await response.Content.ReadAsStringAsync();


            
        }

        [Fact]
        public void CanDeserialize()
        {
            const string message = @"<Request valid=""1"">
                    <URI>https://sec.paymentexpress.com/pxmi3/EF4054F622D6C4C1BC0B7C172FA2C7B90758029292E42AB5B804B68BD379A3F18D5F3D16E6560F51A</URI>
                </Request>";

            var xmlSerializer = new XmlSerializer(typeof (RequestMessage));
            var xmlReader = XmlReader.Create(new StringReader(message));
            var xml = xmlSerializer.Deserialize(xmlReader) as RequestMessage;
        }
    }

    [XmlRoot(ElementName = "Request", Namespace = "")]
    public class RequestMessage
    {
        [XmlAttribute(AttributeName = "valid")]
        public int Valid { get; set; }
        [XmlElement(ElementName = "URI")]
        public string Uri { get; set; }
    }

    public interface IPaymentExpressApi
    {
        [Post("/pxaccess/pxpay.aspx")]
        [Headers("Content-Type: text/xml")]
        Task<HttpResponseMessage> GenerateRequest([Body] string generateRequestXml);
    }


    
}
