using DpsPayfit.Client;
using DpsPayfit.Validation;
using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace DpsPayfit.Test
{
    public class DpsPayfitTest
    {
        [Fact]
        public void ValidatesGenerateRequest()
        {
            var message = new GenerateRequestMessage();
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
        public void ThrowsExceptionWhenMessgaeIsInvalid()
        {
            const string error = "Sample error";
            var message = new GenerateRequestMessage();
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
    }

    public class DpsPayfitFixture
    {
        public IDataAnnotationsValidator Validator { get; set; }
        public IPaymentExpressApi Api { get; private set; }

        public DpsPayfitFixture()
        {
            var mock = new Mock<IDataAnnotationsValidator>();
            mock.Setup(m => m.Validate(It.IsAny<object>())).Returns(Enumerable.Empty<PropertyValidationResult>());
            Validator = mock.Object;
            Api = new Mock<IPaymentExpressApi>().Object;
        }

        public DpsPayfit Build()
        {
            return new DpsPayfit(Validator, Api);
        }
    }
}
