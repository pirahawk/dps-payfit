using DpsPayfit.Client;
using DpsPayfit.Client.Test;
using DpsPayfit.Validation;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DpsPayfit.Test
{
    public class DpsPayfitTest
    {
        [Fact]
        public void ValidatesGenerateRequest()
        {
            var message = new GenerateRequestMessageFixture().Build();
            var validator = new Mock<IDataAnnotationsValidator>();
            validator.Setup(m => m.Validate(message)).Returns(Enumerable.Empty<PropertyValidationResult>()).Verifiable();
            var payfit = new DpsPayfitFixture
            {
                Validator = validator.Object
            }.Build();

            payfit.EnsureMessageValid(message);
            validator.VerifyAll();
        }

        [Fact]
        public void ThrowsExceptionWhenMessageIsInvalid()
        {
            const string error = "Sample error";
            var message = new GenerateRequestMessageFixture().Build();
            var invalidMemberName = nameof(message.EmailAddress);
            var validator = new Mock<IDataAnnotationsValidator>();
            validator.Setup(m => m.Validate(message)).Returns(new[] { new PropertyValidationResult {
                IsValid = false,
                MemberName = invalidMemberName,
                ValidationResults = new[] {new  ValidationResult(error) }
            } }).Verifiable();
            var payfit = new DpsPayfitFixture
            {
                Validator = validator.Object
            }.Build();

            Assert.Throws <Exception>(() => {
                payfit.EnsureMessageValid(message);
            });

            validator.VerifyAll();
        }

        [Fact]
        public async Task CallsApiToGenerateRequest() {
            var message = new GenerateRequestMessageFixture().Build();
            var response = new RequestMessageFixture().Build();
            var validator = new Mock<IDataAnnotationsValidator>();
            validator.Setup(m => m.Validate(message))
                .Returns(Enumerable.Empty<PropertyValidationResult>())
                .Verifiable();
            var api = new Mock<IPaymentExpressApi>();
            api.Setup(m => m.PostGenerateRequestAsync(message))
                .ReturnsAsync(response)
                .Verifiable();

            var payfit = new DpsPayfitFixture
            {
                Api = api.Object,
                Validator = validator.Object
            }.Build();

            var result = await payfit.CreateGenerateRequest(message);

            Assert.Equal(response, result);
            validator.VerifyAll();
            api.VerifyAll();
        }
    }

    public class DpsPayfitFixture
    {
        public IDataAnnotationsValidator Validator { get; set; }
        public IPaymentExpressApi Api { get; set; }

        public DpsPayfitFixture()
        {
            Api = new Mock<IPaymentExpressApi>().Object;
            var mock = new Mock<IDataAnnotationsValidator>();
            mock.Setup(m => m.Validate(It.IsAny<object>())).Returns(Enumerable.Empty<PropertyValidationResult>());
            Validator = mock.Object;
        }

        public DpsPayfit Build()
        {
            return new DpsPayfit(Validator, Api);
        }
    }
}
