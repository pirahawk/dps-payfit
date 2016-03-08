using System;
using System.Net;
using System.Threading.Tasks;
using Refit;

namespace DpsPayfit.Client
{
    internal class PaymentExpressApi : IPaymentExpressApi
    {
        private readonly string _hostName;

        public PaymentExpressApi(string hostName)
        {
            if (hostName == null) throw new ArgumentNullException(nameof(hostName));
            _hostName = hostName;
        }

        public async Task<string> PostGenerateRequestAsync(GenerateRequestMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            var service = RestService.For<IGenerateRequest>(_hostName);
            var generateRequestXml = XmlMessageSerializer.SerializeToXml(message);
            var response = await service.GenerateRequest(generateRequestXml);
            var responseBody = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"PaymentExpress GenerateRequest returned STATUS: {response.StatusCode} \n MessageBody:\n {responseBody}");
            }
            return responseBody;
        }
    }
}