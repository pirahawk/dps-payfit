using DpsPayfit.Validation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DpsPayfit.Client.Test
{
    public class GenerateRequestMessageTest
    {
        [Theory, MemberData("AmountInputData")]
        public void AmountInputFormatAccountsForCurrencyType(GenerateRequestMessage message, string expectedAmountInput)
        {
            Assert.Equal(expectedAmountInput, message.AmountInput);
        }

        public static IEnumerable<object[]> AmountInputData
        {
            get
            {
                var allCurrenciesExceptJpyYen = Enum.GetValues(typeof (Currency))
                    .Cast<Currency>()
                    .Where(c => c != Currency.JPY).ToArray();
                foreach (var currency in allCurrenciesExceptJpyYen)
                {
                    yield return new object[] { new GenerateRequestMessageFixture { Amount = 5m, CurrencyInput = currency }.Build(), "5.00" };
                    yield return new object[] { new GenerateRequestMessageFixture { Amount = 5.544m, CurrencyInput = currency }.Build(), "5.54" };
                    yield return new object[] { new GenerateRequestMessageFixture { Amount = 5.5m, CurrencyInput = currency }.Build(), "5.50" };
                    yield return new object[] { new GenerateRequestMessageFixture { Amount = 5.545m, CurrencyInput = currency }.Build(), "5.55" };
                }
                yield return new object[] { new GenerateRequestMessageFixture { Amount = 5m, CurrencyInput = Currency.JPY }.Build(), "5" };
                yield return new object[] { new GenerateRequestMessageFixture { Amount = 5.445m, CurrencyInput = Currency.JPY }.Build(), "5" };
                yield return new object[] { new GenerateRequestMessageFixture { Amount = 5.5m, CurrencyInput = Currency.JPY }.Build(), "6" };
                yield return new object[] { new GenerateRequestMessageFixture { Amount = 5.545m, CurrencyInput = Currency.JPY }.Build(), "6" };
            }
        }


        [Theory, MemberData("PropertyRulesData")]
        public void PropertyAnnotationsHoldAsExpected(GenerateRequestMessage message, string propertyName, bool expectValid)
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

        public static IEnumerable<object[]> PropertyRulesData
        {
            get
            {
                var message = new GenerateRequestMessageFixture().Build();

                yield return new object[] { new GenerateRequestMessageFixture { }.Build(), nameof(message.PxPayKey), true };

                yield return new object[] { new GenerateRequestMessageFixture { Amount = -1 }.Build(), nameof(message.Amount), false };
                yield return new object[] { new GenerateRequestMessageFixture { Amount = 1000000 }.Build(), nameof(message.Amount), false };

                yield return new object[] { new GenerateRequestMessageFixture { EmailAddress = "test@google.co.nz" }.Build(), nameof(message.EmailAddress), true };
                yield return new object[] { new GenerateRequestMessageFixture { EmailAddress = "notAnEmail" }.Build(), nameof(message.EmailAddress), false };

                yield return new object[] { new GenerateRequestMessageFixture { EnableAddBillCard = null}.Build(), nameof(message.EnableAddBillCard), true };
                yield return new object[] { new GenerateRequestMessageFixture { EnableAddBillCard = 0 }.Build(), nameof(message.EnableAddBillCard), true };
                yield return new object[] { new GenerateRequestMessageFixture { EnableAddBillCard = 1 }.Build(), nameof(message.EnableAddBillCard), true };
                yield return new object[] { new GenerateRequestMessageFixture { EnableAddBillCard = 8 }.Build(), nameof(message.EnableAddBillCard), false };
            }
        }

        [Fact]
        public void SetsTxnTypeAsExpected()
        {
            var auth = new GenerateRequestMessageFixture
            {
                TxnType = TxnType.Auth
            }.Build();

            var purchase = new GenerateRequestMessageFixture
            {
                TxnType = TxnType.Purchase
            }.Build();

            Assert.Equal(TxnType.Auth.ToString(), auth.TxnType);
            Assert.Equal(TxnType.Purchase.ToString(), purchase.TxnType);
        }

        [Fact]
        public void SetsClientTypeAsExpected()
        {
            var internet = new GenerateRequestMessageFixture
            {
                ClientType = ClientType.Internet
            }.Build();

            var recurring = new GenerateRequestMessageFixture
            {
                ClientType = ClientType.Recurring
            }.Build();

            Assert.Equal(ClientType.Internet.ToString(), internet.ClientType);
            Assert.Equal(ClientType.Recurring.ToString(), recurring.ClientType);
        }

        [Fact]
        public void SetsTimeoutValueFormatAsExpected()
        {
            var currentTime = DateTimeOffset.UtcNow;
            var withTimeout = new GenerateRequestMessageFixture
            {
                Timeout = currentTime
            }.Build();

            var withoutTimeout = new GenerateRequestMessageFixture
            {
                Timeout = null
            }.Build();

            Assert.Equal($"{currentTime:yyMMddHHmm}", withTimeout.Timeout);
            Assert.Null(withoutTimeout.Timeout);
        }
    }
}