namespace DpsPayfit
{
    public enum TxnType
    {
        /// <summary>
        /// A Purchase transaction type processes a financial transaction immediately and funds are transferred at next settlement cut-off
        /// </summary>
        Purchase,

        /// <summary>
        /// An Auth transaction type verifies that funds are available for the requested card and amount and reserves the specified amount. The amount is reserved for a period of up to 7 days (depending on merchant bank) before it clears.
        /// </summary>
        Auth,
    }

    /// <summary>
    /// Allows the Client type to be passed through at transaction time
    /// </summary>
    public enum ClientType
    {
        /// <summary>
        /// Internet
        /// </summary>
        Internet,
        /// <summary>
        /// Recurring (if supported by your MID at your merchant bank expiry date will not be validated)
        /// </summary>
        Recurring

    }
}