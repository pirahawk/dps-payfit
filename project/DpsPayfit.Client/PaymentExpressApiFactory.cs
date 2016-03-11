using Refit;
using System;

namespace DpsPayfit.Client
{
    public class PaymentExpressApiFactory
    {
        public IPaymentExpressApi CreateApi(string hostName)
        {
            if (hostName == null) throw new ArgumentNullException(nameof(hostName));
            var service = RestService.For<IGenerateRequest>(hostName);
            return new PaymentExpressApi(service);
        }
    }
}