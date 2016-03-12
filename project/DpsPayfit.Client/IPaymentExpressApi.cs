using System.Threading.Tasks;

namespace DpsPayfit.Client
{
    public interface IPaymentExpressApi
    {
        Task<RequestMessage> PostGenerateRequestAsync(GenerateRequestMessage message);
    }
}