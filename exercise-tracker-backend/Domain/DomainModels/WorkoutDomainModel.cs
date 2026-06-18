namespace Domain.DomainModels
{
    public enum ExerciseType
    {
        Cardio,
        StrengthTraining,
        Flexibility,
        Aerobic,
        Yoga,
        Other
    }
    public class WorkoutDomainModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public UserDomainModel User { get; set; }
        public ExerciseType Type { get; set; }
        public int DurationInMinutes { get; set; }
        public int CaloriesBurned { get; set; }
        public int IntensityLevel { get; set; }
        public int FatigueLevel { get; set; }
        public DateTime TrainingDateTime { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
