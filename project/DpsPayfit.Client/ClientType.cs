namespace DpsPayfit.Client
{
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