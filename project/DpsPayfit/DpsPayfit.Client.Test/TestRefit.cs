using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DpsPayfit.Client.Test
{
    public class TestRefit
    {
        [Fact]
        public async Task RefitoWrks()
        {
            const string message = @"<GenerateRequest>
                <PxPayUserId>Sample User</PxPayUserId>
                <PxPayKey>Sample Key</PxPayKey>
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
            HttpResponseMessage response = null;
            //Task.Run(async ()=> response = await service.GenerateRequest(message));
            response = await service.GenerateRequest(message);
        }
    }

    public interface IPaymentExpressApi
    {
        [Post("/pxaccess/pxpay.aspx")]
        [Headers("Content-Type: text/xml")]
        Task<HttpResponseMessage> GenerateRequest([Body] string generateRequestXml);
    }
}
