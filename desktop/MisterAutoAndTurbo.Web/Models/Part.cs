namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// Inventory part.
    /// </summary>
    public class Part
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
    }
}
