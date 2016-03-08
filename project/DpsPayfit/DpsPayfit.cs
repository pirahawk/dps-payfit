using System;
using System.Linq;
using System.Threading.Tasks;
using DpsPayfit.Validation;
using DpsPayfit.Client;

namespace DpsPayfit
{
    public class DpsPayfit
    {
        private readonly IDataAnnotationsValidator _validator;
        private readonly IPaymentExpressApi _api;

        public DpsPayfit(IDataAnnotationsValidator validator, IPaymentExpressApi api)
        {
            if (validator == null) throw new ArgumentNullException(nameof(validator));
            if (api == null) throw new ArgumentNullException(nameof(api));

            _validator = validator;
            _api = api;
        }


        public async Task CreateGenerateRequest(GenerateRequestMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            EnsureMessageValid(message);
            var response = await _api.PostGenerateRequestAsync(message);
        }


        internal void EnsureMessageValid<TMessage>(TMessage requestMessage)
        {
            var results = _validator.Validate(requestMessage);
            var allResultsValid = results.All(r => r.IsValid);
            if (allResultsValid)
            {
                return;
            }
            var propertyValidationErrors = results
                .SelectMany(result => result.ValidationResults.Select(r => $"{result.MemberName}: {r.ErrorMessage}"));

            var exceptionMessage = $"Message of type:{requestMessage.GetType().Name} has failed validation on the following properties:\n" +
                                   $"{string.Join("\n", propertyValidationErrors)}";
            throw new Exception(exceptionMessage);
        }
    }
}