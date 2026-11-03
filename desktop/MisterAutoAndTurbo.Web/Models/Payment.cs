namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// Payment record for a job or quotation.
    /// </summary>
    public class Payment
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string? JobCardId { get; set; }
        public string? QuotationId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public DateTime PaidAt { get; set; }
        public string? Reference { get; set; }
        public string RecordedById { get; set; } = string.Empty;
    }
}
