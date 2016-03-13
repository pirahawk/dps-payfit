using DpsPayfit.Validation.Test;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DpsPayfit.Client.Test
{
    public class ProcessResponseMessageTest
    {
        [Theory, MemberData("ProcessResponseMessageData")]
        public void RaisesValidationErrorsOnFields(ProcessResponseMessage message, string propertyName, bool expectValid)
        {
            var validator = new DataAnnotationsValidatorFixture().Build();
            var results = validator.Validate(message);
            if (expectValid)
            {
                Assert.True(results.Any(r => r.MemberName != propertyName));
            }
            else
            {
                var propResult = results.Single(r => r.MemberName == propertyName);
                Assert.False(propResult.IsValid);
                Assert.True(propResult.ValidationResults.Any());
            }
        }

        public static IEnumerable<object[]> ProcessResponseMessageData
        {
            get
            {
                var message = new ProcessResponseMessageFixture().Build();
                yield return new object[] { new ProcessResponseMessageFixture { }.Build(), nameof(message.PxPayKey), true };

                yield return new object[] { new ProcessResponseMessageFixture { PxPayKey = string.Empty }.Build(), nameof(message.PxPayKey), false };
                yield return new object[] { new ProcessResponseMessageFixture { PxPayUserId = string.Empty }.Build(), nameof(message.PxPayUserId), false };
                yield return new object[] { new ProcessResponseMessageFixture { Response = string.Empty }.Build(), nameof(message.Response), false };
            }
        }
    }
}
