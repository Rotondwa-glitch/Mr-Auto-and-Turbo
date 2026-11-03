namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// A customer complaint about a job or vehicle service.
    /// </summary>
    public class Complaint
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string? JobCardId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";
        public DateTime CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public string? ResolutionNotes { get; set; }
    }
}
