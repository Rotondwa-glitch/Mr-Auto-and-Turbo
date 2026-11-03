namespace MisterAutoAndTurbo.Web.Models
{
    /// <summary>
    /// A customer of the workshop: contact details and optional linked user account.
    /// </summary>
    public class Customer
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        /// <summary>Optional link to a UserAccount if the customer can log in.</summary>
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
