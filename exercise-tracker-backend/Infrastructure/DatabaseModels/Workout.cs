using Domain.DomainModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.DatabaseModels;

public class Workout
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public long UserId { get; set; }
    [ForeignKey("UserId")]
    public User User { get; set; }

    [Required]
    public ExerciseType Type { get; set; }

    [Required]
    [Range(1, 1440, ErrorMessage = "Duration must be between 1 minute and 24 hours.")]
    public int DurationInMinutes { get; set; }

    [Range(0, 5000, ErrorMessage = "Calories burned must be a positive number.")]
    public int CaloriesBurned { get; set; }

    [Required]
    [Range(1, 10, ErrorMessage = "Intensity weight must be on a scale of 1 to 10.")]
    public int IntensityLevel { get; set; }

    [Required]
    [Range(1, 10, ErrorMessage = "Fatigue level must be on a scale of 1 to 10.")]
    public int FatigueLevel { get; set; }

    [Required]
    public DateTime TrainingDateTime { get; set; }

    [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}