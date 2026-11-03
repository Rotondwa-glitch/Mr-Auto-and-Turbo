namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// Simple pricing entry.
    /// </summary>
    public class Pricing
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
