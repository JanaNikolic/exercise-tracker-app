namespace Domain.DomainModels
{
    public class WorkoutCreationDomainModel
    {
        public ExerciseType Type { get; set; }
        public int DurationInMinutes { get; set; }
        public int CaloriesBurned { get; set; }
        public int IntensityLevel { get; set; }
        public int FatigueLevel { get; set; }
        public DateTime TrainingDateTime { get; set; }
        public string Notes { get; set; } = string.Empty;
        public long UserId { get; set; }
    }
}
