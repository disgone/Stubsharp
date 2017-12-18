namespace Stubsharp.Models.Common
{
    public interface IMoney
    {
        /// <summary>
        /// The amount of currency units
        /// </summary>
        /// <value>The amount.</value>
        decimal Amount { get; set; }

        /// <summary>
        /// The ISO-4217 currency code
        /// </summary>
        /// <value>The currency code.</value>
        string CurrencyCode { get; set; }
    }
}
