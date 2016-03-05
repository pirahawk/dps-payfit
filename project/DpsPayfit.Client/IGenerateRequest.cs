using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace DpsPayfit.Client
{
    public interface IGenerateRequest
    {
        [Post("/pxaccess/pxpay.aspx")]
        [Headers("Content-Type: text/xml")]
        Task<HttpResponseMessage> GenerateRequest([Body] string generateRequestXml);
    }
}