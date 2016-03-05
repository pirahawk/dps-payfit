using System;
using System.Threading.Tasks;
using Refit;

namespace DpsPayfit.Client
{
    public class PaymentExpressApi
    {
        private readonly string _hostName;

        public PaymentExpressApi(string hostName)
        {
            if (hostName == null) throw new ArgumentNullException(nameof(hostName));
            _hostName = hostName;
        }

        public async Task<string> PostGenerateRequestAsync(string generateRequestXml)
        {
            if (generateRequestXml == null) throw new ArgumentNullException(nameof(generateRequestXml));
            var service = RestService.For<IGenerateRequest>(_hostName);
            var response = await service.GenerateRequest(generateRequestXml);
            return response;
        }
    }
}