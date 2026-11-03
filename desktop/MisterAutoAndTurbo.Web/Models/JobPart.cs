namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// A part used on a job card.
    /// </summary>
    public class JobPart
    {
        public string Id { get; set; } = string.Empty;
        public string JobCardId { get; set; } = string.Empty;
        public string PartId { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
