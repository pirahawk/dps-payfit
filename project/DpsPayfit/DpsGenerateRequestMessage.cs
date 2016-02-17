using System;
using System.ComponentModel.DataAnnotations;

namespace DpsPayfit
{
    /// <summary>
    /// Represents a Dps Generate-Request Message
    /// </summary>
    public class DpsGenerateRequestMessage : IDpsMessage
    {
        public DpsGenerateRequestMessage(string pxPayUserId,
            string pxPayKey,
            string urlSuccess,
            string urlFail,
            Currency currencyInput,
            TxnType txnType,
            decimal amount = 0,
            ClientType clientType = DpsPayfit.ClientType.Internet,
            DateTimeOffset? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(pxPayUserId)) throw new ArgumentNullException(nameof(pxPayUserId));
            if (string.IsNullOrWhiteSpace(pxPayKey)) throw new ArgumentNullException(nameof(pxPayKey));
            if (string.IsNullOrWhiteSpace(urlSuccess)) throw new ArgumentNullException(nameof(urlSuccess));
            if (string.IsNullOrWhiteSpace(urlFail)) throw new ArgumentNullException(nameof(urlFail));

            PxPayUserId = pxPayUserId;
            PxPayKey = pxPayKey;
            UrlSuccess = urlSuccess;
            UrlFail = urlFail;
            Amount = amount;
            CurrencyInput = currencyInput;
            TxnType = txnType.ToString();
            ClientType = clientType.ToString();
            if (timeout.HasValue)
            {
                Timeout = $"{timeout:yyMMddHHmm}";
            }
        }

        public string PxPayUserId { get;}
        public string PxPayKey { get; }
        public string AmountInput
        {
            get
            {
                return CurrencyInput == Currency.JPY ? $"{Amount:F0}" : $"{Amount:F2}";
            }
        }

        [Range(0, 999999.99)]
        public decimal Amount { get; set; }
        public Currency CurrencyInput { get; set; }
        public string TxnType { get; }
        public string UrlFail { get; }
        public string UrlSuccess { get; }
        

        [EmailAddress]
        public string EmailAddress { get; set; }
        [Range(0, 1)]
        public int? EnableAddBillCard { get; set; }
        public string MerchantReference { get; set; }
        public string DpsBillingId { get; set; }
        public string TxnData1 { get; set; }
        public string TxnData2 { get; set; }
        public string TxnData3 { get; set; }
        public string TxnId { get; set; }
        public string Opt { get; set; }
        public string ClientType { get; }
        public string Timeout { get;}
    }
}
