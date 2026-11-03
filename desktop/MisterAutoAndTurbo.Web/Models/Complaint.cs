namespace MisterAutoAndTurbo.Web.Models
{
    public class Complaint
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string JobCardId { get; set; } = string.Empty;
        public string VehicleId { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";
        public string Source { get; set; } = "Staff";
        public string StaffNotes { get; set; } = string.Empty;
        public string ResolutionNotes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
    }
}
