namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// A job card tracking work on a vehicle.
    /// </summary>
    public class JobCard
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string VehicleId { get; set; } = string.Empty;
        public string? AppointmentId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? AssignedMechanicId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
