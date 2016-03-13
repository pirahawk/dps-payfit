using System;

namespace DpsPayfit.Client.Test
{
    public class GenerateRequestMessageFixture
    {
        public GenerateRequestMessageFixture()
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
        public GenerateRequestMessage Build()
        {
            return new GenerateRequestMessage(Timeout = Timeout )
            {
                PxPayUserId = PxPayUserId,
                PxPayKey = PxPayKey,
                UrlSuccess = UrlSuccess,
                UrlFail = UrlFail,
                CurrencyInput = CurrencyInput,
                TxnType = TxnType,
                Amount = Amount,
                ClientType = ClientType,
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
