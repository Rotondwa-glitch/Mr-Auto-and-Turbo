namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// Repair work item on a job card.
    /// </summary>
    public class Repair
    {
        public string Id { get; set; } = string.Empty;
        public string JobCardId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal LabourCost { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
