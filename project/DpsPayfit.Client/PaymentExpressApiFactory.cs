using System;

namespace DpsPayfit.Client
{
    public class PaymentExpressApiFactory
    {
        public IPaymentExpressApi CreateApi(string hostName)
        {
            if (hostName == null) throw new ArgumentNullException(nameof(hostName));
            return new PaymentExpressApi(hostName);
        }
    }
}