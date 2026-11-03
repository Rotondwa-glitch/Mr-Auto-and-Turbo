namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// Incoming service request from a customer.
    /// </summary>
    public class ServiceRequest
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string VehicleId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "New";
        public DateTime CreatedAt { get; set; }
    }
}
