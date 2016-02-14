using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DpsPayfit.Test
{
    public class DpsGenerateRequestMessageTest
    {
        [Theory, MemberData("PropertyRulesData")]
        public void PropertyAnnotationsHoldAsExpected(DpsGenerateRequestMessage message, string propertyName, bool expectValid)
        {
            var dataValidationResult = message.Validate();
            if (expectValid)
            {
                Assert.True(dataValidationResult.IsValid);
            }
            else
            {
                var propResult = dataValidationResult[propertyName];
                Assert.False(dataValidationResult.IsValid);
                Assert.False(propResult.IsValid);
            }
        }

        public static IEnumerable<object[]> PropertyRulesData
        {
            get
            {
                var withKey = new DpsGenerateRequestMessageFixture {}.Build();
                yield return new object[] { withKey, nameof(withKey.PxPayKey), true };

                var withoutKey = new DpsGenerateRequestMessageFixture { PxPayKey = string.Empty}.Build();
                yield return new object[] { withoutKey, nameof(withKey.PxPayKey), false };

                var withoutUserId = new DpsGenerateRequestMessageFixture { PxPayUserId = string.Empty }.Build();
                yield return new object[] { withoutUserId, nameof(withKey.PxPayUserId), false };

                var incorrectAmountLowerBound = new DpsGenerateRequestMessageFixture { Amount = -1 }.Build();
                yield return new object[] { incorrectAmountLowerBound, nameof(withKey.Amount), false };

                var incorrectAmountUpperBound = new DpsGenerateRequestMessageFixture { Amount = 1000000 }.Build();
                yield return new object[] { incorrectAmountUpperBound, nameof(withKey.Amount), false };
            }
        }
    }

    public class DpsGenerateRequestMessageFixture
    {
        public DpsGenerateRequestMessageFixture()
        {
            PxPayKey = "aKey";
            PxPayUserId = "anId";
            Amount = 10;
            CurrencyInput = Currency.AUD;
        }
        public DpsGenerateRequestMessage Build()
        {
            return new DpsGenerateRequestMessage
            {
                PxPayKey = PxPayKey,
                PxPayUserId = PxPayUserId,
                Amount = Amount,
                CurrencyInput = CurrencyInput,
            };
        }
        public Currency CurrencyInput { get; set; }
        public decimal Amount { get; set; }
        public string PxPayUserId { get; set; }
        public string PxPayKey { get; set; }
    }
}