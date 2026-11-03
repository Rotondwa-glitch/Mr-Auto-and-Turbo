namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// Status values for quotations.
    /// </summary>
    public static class QuotationStatus
    {
        public const string Draft = "Draft";
        public const string Sent = "Sent";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
        public const string Expired = "Expired";

        public static readonly string[] All = { Draft, Sent, Approved, Rejected, Expired };
    }
}
