using System.ComponentModel.DataAnnotations;

namespace MisterAutoAndTurbo.Web.ViewModels
{
    public class UserCreateViewModel
    {
        [Required, Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;
    }

    public class UserEditViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required, Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        [MinLength(6)]
        public string? NewPassword { get; set; }
    }
}
