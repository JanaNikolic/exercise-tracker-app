using Domain.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class WorkoutForCreationDto
{
    [Required(ErrorMessage = "Exercise type is required.")]
    public ExerciseType Type { get; set; }

    [Required]
    [Range(1, 1440, ErrorMessage = "Duration must be between 1 and 1440 minutes.")]
    public int DurationInMinutes { get; set; }

    [Range(0, 5000, ErrorMessage = "Calories burned cannot be negative.")]
    public int CaloriesBurned { get; set; }

    [Required]
    [Range(1, 10, ErrorMessage = "Intensity must be ranked from 1 (very easy) to 10 (maximal effort).")]
    public int IntensityLevel { get; set; }

    [Required]
    [Range(1, 10, ErrorMessage = "Fatigue must be ranked from 1 (fresh) to 10 (exhausted).")]
    public int FatigueLevel { get; set; }

    [Required(ErrorMessage = "Training date and time is required.")]
    public DateTime TrainingDateTime { get; set; }

    [MaxLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
    public string Notes { get; set; } = string.Empty;
}