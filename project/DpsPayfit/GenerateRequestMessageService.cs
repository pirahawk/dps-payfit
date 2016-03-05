using System;
using System.Linq;
using System.Threading.Tasks;
using DpsPayfit.Validation;

namespace DpsPayfit
{
    public class GenerateRequestMessageService
    {
        private readonly IDataAnnotationsValidator _validator;
        private readonly IXmlMessageSerializer _serializer;

        public GenerateRequestMessageService(IDataAnnotationsValidator validator, IXmlMessageSerializer serializer)
        {
            if (validator == null) throw new ArgumentNullException(nameof(validator));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));
            _validator = validator;
            _serializer = serializer;
        }


        public void CreateGenerateRequest(GenerateRequestMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            EnsureMessageValid(message);
           
        }


        private void EnsureMessageValid<TMessage>(TMessage requestMessage)
        {
            var results = _validator.Validate(requestMessage);
            if (!results.Any())
            {
                return;
            }
            var propertyValidationErrors = results
                .SelectMany(result => result.ValidationResults
                    .Select(r => $"{r.MemberNames.FirstOrDefault()}: {r.ErrorMessage}"));
            var exceptionMessage = $"Message of type:{requestMessage.GetType().Name} has failed validation on the following properties:\n" +
                                   $"{string.Join("\n", propertyValidationErrors)}";
            throw new Exception(exceptionMessage);
        }
    }
}