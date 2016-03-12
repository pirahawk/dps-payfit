using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DpsPayfit.Client.Test
{
    public class PaymentExpressApiTest
    {
        const string MockResponse = @"<Request valid=""1""><URI>https://test.com/123</URI></Request>";
        [Theory, MemberData("ExpectedResponseCodes")]
        public async Task ConductsGenertaeRequestMessageAsExpected(HttpStatusCode statusCode)
        {
            var message = new GenerateRequestMessageFixture().Build();
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(MockResponse)
            };
            var service = new Mock<IGenerateRequest>();
            service.Setup(m => m.GenerateRequest(It.IsAny<string>())).ReturnsAsync(response).Verifiable();
            var api = new PaymentExpressApiFixture
            {
                GenerateRequestService = service.Object
            }.Build();
            if (statusCode == HttpStatusCode.OK)
            {
                var result = await api.PostGenerateRequestAsync(message);
                Assert.True(result.IsValid);
            }
            else
            {
                await Assert.ThrowsAsync<Exception>(() => api.PostGenerateRequestAsync(message));
            }
            service.VerifyAll();
        }

        public static IEnumerable<object[]> ExpectedResponseCodes
        {
            get
            {
                yield return new object[] { HttpStatusCode.OK };
                yield return new object[] { HttpStatusCode.NotFound };
            }
        }
    }

    public class PaymentExpressApiFixture
    {
        public IGenerateRequest GenerateRequestService { get; set; }
        public PaymentExpressApiFixture()
        {
            GenerateRequestService = new Mock<IGenerateRequest>().Object;
        }

        public PaymentExpressApi Build()
        {
            return new PaymentExpressApi(GenerateRequestService);
        }
    }
}
