using System.ComponentModel.DataAnnotations;

namespace MisterAutoAndTurbo.Web.ViewModels
{
    public class SettingsViewModel
    {
        [Required, Display(Name = "Business Name")]
        public string BusinessName { get; set; } = string.Empty;

        [Display(Name = "VAT Number")]
        public string? VatNumber { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Required]
        public string Currency { get; set; } = "ZAR";

        [Display(Name = "VAT Rate (%)")]
        [Range(0, 100)]
        public decimal? VatRate { get; set; }
    }
}
