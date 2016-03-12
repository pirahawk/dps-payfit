using System;
using System.Net;
using System.Threading.Tasks;
using Refit;

namespace DpsPayfit.Client
{
    public class PaymentExpressApi : IPaymentExpressApi
    {
        private IGenerateRequest _generateRequestService;
        public PaymentExpressApi(IGenerateRequest generateRequestService)
        {
            if (generateRequestService == null) throw new ArgumentNullException(nameof(generateRequestService));
            _generateRequestService = generateRequestService;
        }

        public async Task<RequestMessage> PostGenerateRequestAsync(GenerateRequestMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            var generateRequestXml = XmlMessageSerializer.Serialize(message);
            var response = await _generateRequestService.GenerateRequest(generateRequestXml);
            var responseBody = response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"PaymentExpress GenerateRequest returned STATUS: {response.StatusCode} \n MessageBody:\n {responseBody}");
            }
            var responseBodyXml = await responseBody;
            return XmlMessageSerializer.Deserialize<RequestMessage>(responseBodyXml);
        }
    }
}