namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// Quotation sent to a customer for approval.
    /// </summary>
    public class Quotation
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string VehicleId { get; set; } = string.Empty;
        public string? JobCardId { get; set; }
        public string Status { get; set; } = "Draft";
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public string CreatedById { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
