namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// Vehicle inspection record linked to a job card.
    /// </summary>
    public class Inspection
    {
        public string Id { get; set; } = string.Empty;
        public string JobCardId { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime InspectedAt { get; set; }
        public string InspectedById { get; set; } = string.Empty;
    }
}
