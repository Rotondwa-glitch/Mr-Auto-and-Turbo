namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// Inspection result recorded by a mechanic for a job card.
    /// </summary>
    public class Inspection
    {
        public string Id { get; set; } = string.Empty;
        public string JobCardId { get; set; } = string.Empty;
        public string MechanicId { get; set; } = string.Empty;
        public string Findings { get; set; } = string.Empty;
        public string RecommendedAction { get; set; } = string.Empty;
        public DateTime InspectedAt { get; set; }
    }
}
