using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.DatabaseModels
{
    [Index(nameof(Name), nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
