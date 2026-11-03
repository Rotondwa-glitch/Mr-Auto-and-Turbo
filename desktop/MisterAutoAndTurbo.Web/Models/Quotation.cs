namespace MisterAutoAndTurbo.Web.Models
{
    public class Quotation
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string VehicleId { get; set; } = string.Empty;
        public string Status { get; set; } = "Draft";
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string CreatedById { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
