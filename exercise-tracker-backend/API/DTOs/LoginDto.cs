using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required.", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Email must be between 1 and 50 characters.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Email must be between 8 and 50 characters.")]
        public string Password { get; set; }
    }
}