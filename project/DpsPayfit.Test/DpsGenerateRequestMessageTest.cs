using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DpsPayfit.Test
{
    public class DpsGenerateRequestMessageTest
    {
        [Theory, MemberData("AmountInputData")]
        public void AmountInputFormatAccountsForCurrencyType(DpsGenerateRequestMessage message, string expectedAmountInput)
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
                    yield return new object[] { new DpsGenerateRequestMessageFixture { Amount = 5m, CurrencyInput = currency }.Build(), "5.00" };
                    yield return new object[] { new DpsGenerateRequestMessageFixture { Amount = 5.544m, CurrencyInput = currency }.Build(), "5.54" };
                    yield return new object[] { new DpsGenerateRequestMessageFixture { Amount = 5.5m, CurrencyInput = currency }.Build(), "5.50" };
                    yield return new object[] { new DpsGenerateRequestMessageFixture { Amount = 5.545m, CurrencyInput = currency }.Build(), "5.55" };
                }
                yield return new object[] { new DpsGenerateRequestMessageFixture { Amount = 5m, CurrencyInput = Currency.JPY }.Build(), "5" };
                yield return new object[] { new DpsGenerateRequestMessageFixture { Amount = 5.445m, CurrencyInput = Currency.JPY }.Build(), "5" };
                yield return new object[] { new DpsGenerateRequestMessageFixture { Amount = 5.5m, CurrencyInput = Currency.JPY }.Build(), "6" };
                yield return new object[] { new DpsGenerateRequestMessageFixture { Amount = 5.545m, CurrencyInput = Currency.JPY }.Build(), "6" };
            }
        }


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
                var message = new DpsGenerateRequestMessageFixture().Build();

                yield return new object[] { new DpsGenerateRequestMessageFixture { }.Build(), nameof(message.PxPayKey), true };

                yield return new object[] { new DpsGenerateRequestMessageFixture { Amount = -1 }.Build(), nameof(message.Amount), false };
                yield return new object[] { new DpsGenerateRequestMessageFixture { Amount = 1000000 }.Build(), nameof(message.Amount), false };

                yield return new object[] { new DpsGenerateRequestMessageFixture { EmailAddress = "test@google.co.nz" }.Build(), nameof(message.EmailAddress), true };
                yield return new object[] { new DpsGenerateRequestMessageFixture { EmailAddress = "notAnEmail" }.Build(), nameof(message.EmailAddress), false };

                yield return new object[] { new DpsGenerateRequestMessageFixture { EnableAddBillCard = null}.Build(), nameof(message.EnableAddBillCard), true };
                yield return new object[] { new DpsGenerateRequestMessageFixture { EnableAddBillCard = 0 }.Build(), nameof(message.EnableAddBillCard), true };
                yield return new object[] { new DpsGenerateRequestMessageFixture { EnableAddBillCard = 1 }.Build(), nameof(message.EnableAddBillCard), true };
                yield return new object[] { new DpsGenerateRequestMessageFixture { EnableAddBillCard = 8 }.Build(), nameof(message.EnableAddBillCard), false };
            }
        }

        [Fact]
        public void SetsTxnTypeAsExpected()
        {
            var auth = new DpsGenerateRequestMessageFixture
            {
                TxnType = TxnType.Auth
            }.Build();

            var purchase = new DpsGenerateRequestMessageFixture
            {
                TxnType = TxnType.Purchase
            }.Build();

            Assert.Equal(TxnType.Auth.ToString(), auth.TxnType);
            Assert.Equal(TxnType.Purchase.ToString(), purchase.TxnType);
        }

        [Fact]
        public void SetsClientTypeAsExpected()
        {
            var internet = new DpsGenerateRequestMessageFixture
            {
                ClientType = ClientType.Internet
            }.Build();

            var recurring = new DpsGenerateRequestMessageFixture
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
            var withTimeout = new DpsGenerateRequestMessageFixture
            {
                Timeout = currentTime
            }.Build();

            var withoutTimeout = new DpsGenerateRequestMessageFixture
            {
                Timeout = null
            }.Build();

            Assert.Equal($"{currentTime:yyMMddHHmm}", withTimeout.Timeout);
            Assert.Null(withoutTimeout.Timeout);
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
            EmailAddress = "test@test.com";
            EnableAddBillCard = 1;
            MerchantReference = "merchant-ref";
            TxnType = TxnType.Auth;
            UrlFail = "www.test.com";
            UrlSuccess = "www.test.com";
            ClientType = ClientType.Internet;
            Timeout = DateTimeOffset.UtcNow;
        }
        public DpsGenerateRequestMessage Build()
        {
            return new DpsGenerateRequestMessage(
                PxPayUserId,
                PxPayKey,
                UrlSuccess,
                UrlFail,
                CurrencyInput,
                TxnType,
                Amount,
                ClientType,
                Timeout = Timeout
                )
            {
                EmailAddress = EmailAddress,
                EnableAddBillCard = EnableAddBillCard,
                MerchantReference = MerchantReference,
                TxnData1 = TxnData1,
                TxnData2 = TxnData2,
                TxnData3 = TxnData3,
                TxnId = TxnId,
                Opt = Opt,
            };
        }

        public string Opt { get; set; }
        public string TxnId { get; set; }
        public string TxnData3 { get; set; }
        public string TxnData2 { get; set; }
        public string TxnData1 { get; set; }
        public ClientType ClientType { get; set; }
        public TxnType TxnType { get; set; }
        public string UrlFail { get; set; }
        public string UrlSuccess { get; set; }
        public string MerchantReference { get; set; }
        public int? EnableAddBillCard { get; set; }
        public string EmailAddress { get; set; }
        public Currency CurrencyInput { get; set; }
        public decimal Amount { get; set; }
        public string PxPayUserId { get; set; }
        public string PxPayKey { get; set; }
        public DateTimeOffset? Timeout { get; set; }
    }
}