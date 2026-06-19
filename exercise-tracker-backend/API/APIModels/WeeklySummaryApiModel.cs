namespace API.APIModels
{
    public class WeeklySummaryApiModel
    {
        public int WeekNumber { get; set; }
        public DateTime WeekStart { get; set; }
        public DateTime WeekEnd { get; set; }
        public int TotalDurationInMinutes { get; set; }
        public int TotalNumberOfTrainings { get; set; }
        public double AverageIntensity { get; set; }
        public double AverageFatigue { get; set; }
    }
}
