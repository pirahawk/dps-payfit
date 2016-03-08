using System.Threading.Tasks;

namespace DpsPayfit.Client
{
    public interface IPaymentExpressApi
    {
        Task<string> PostGenerateRequestAsync(GenerateRequestMessage message);
    }
}