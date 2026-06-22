using AutoMapper;
using Domain.DomainModels;
using Domain.IRepositories;
using Domain.IServices;
using System.Globalization;

namespace Application.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;
        public WorkoutService(IWorkoutRepository workoutRepository, IUserService userService, IMapper mapper)
        {
            _workoutRepository = workoutRepository;
        }

        public async Task<WorkoutDomainModel> GetByIdAsync(long id) => await _workoutRepository.GetByIdAsync(id);

        public async Task<IEnumerable<WorkoutDomainModel>> GetUserWorkoutsAsync(long userId) => await _workoutRepository.GetByUserIdAsync(userId);

        public async Task<WorkoutDomainModel> InsertAsync(WorkoutCreationDomainModel workout)
        {
            if (workout.TrainingDateTime > DateTime.UtcNow.AddHours(1))
            {
                throw new ArgumentException("You cannot log a workout that takes place in the future.");
            }
            return await _workoutRepository.InsertAsync(workout);
        }
        public async Task<IEnumerable<WeeklySummaryDomainModel>> GetMonthlyWeeklySummaryAsync(long userId, int year, int month)
        {
            var startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = startDate.AddMonths(1).AddTicks(-1);

            var workouts = await _workoutRepository.GetWorkoutsInDateRangeAsync(userId, startDate, endDate);

            var calendarWeeks = GetWeeksForMonth(year, month);
            var weeklySummaries = new List<WeeklySummaryDomainModel>();

            foreach (var week in calendarWeeks)
            {
                var weekWorkouts = workouts.Where(w => w.TrainingDateTime >= week.Start && w.TrainingDateTime <= week.End).ToList();

                if (weekWorkouts.Any())
                {
                    weeklySummaries.Add(new WeeklySummaryDomainModel
                    {
                        WeekNumber = week.WeekOfMonth,
                        WeekStart = week.Start,
                        WeekEnd = week.End,
                        TotalDurationInMinutes = weekWorkouts.Sum(w => w.DurationInMinutes),
                        TotalNumberOfTrainings = weekWorkouts.Count,
                        AverageIntensity = Math.Round(weekWorkouts.Average(w => w.IntensityLevel), 1),
                        AverageFatigue = Math.Round(weekWorkouts.Average(w => w.FatigueLevel), 1)
                    });
                }
                else
                {
                    weeklySummaries.Add(new WeeklySummaryDomainModel
                    {
                        WeekNumber = week.WeekOfMonth,
                        WeekStart = week.Start,
                        WeekEnd = week.End,
                        TotalDurationInMinutes = 0,
                        TotalNumberOfTrainings = 0,
                        AverageIntensity = 0,
                        AverageFatigue = 0
                    });
                }
            }

            return weeklySummaries.OrderBy(s => s.WeekNumber);
        }
        public async Task<(IEnumerable<WorkoutDomainModel> Items, int TotalCount)> GetPagedUserWorkoutsAsync(long userId, int page, int size)
        {
            if (page < 1) page = 1;
            if (size < 1 || size > 100) size = 10;

            return await _workoutRepository.GetPagedUserWorkoutsAsync(userId, page, size);
        }
        public async Task<bool> DeleteAsync(long id, long userId)
        {
            return await _workoutRepository.DeleteAsync(id, userId);
        }
        private List<CalendarWeekStructure> GetWeeksForMonth(int year, int month)
        {
            var weeks = new List<CalendarWeekStructure>();
            var firstOfMonth = new DateTime(year, month, 1);
            var lastOfMonth = firstOfMonth.AddMonths(1).AddDays(-1);

            DateTime current = firstOfMonth;
            int weekCounter = 1;

            while (current <= lastOfMonth)
            {
                int daysToMonday = ((int)current.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
                DateTime weekStart = current.AddDays(-daysToMonday);

                if (weekStart < firstOfMonth) weekStart = firstOfMonth;

                DateTime weekEnd = weekStart.AddDays(6 - ((int)weekStart.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7);

                if (weekEnd > lastOfMonth) weekEnd = lastOfMonth;

                weeks.Add(new CalendarWeekStructure
                {
                    WeekOfMonth = weekCounter++,
                    Start = new DateTime(weekStart.Year, weekStart.Month, weekStart.Day, 0, 0, 0),
                    End = new DateTime(weekEnd.Year, weekEnd.Month, weekEnd.Day, 23, 59, 59)
                });

                current = weekEnd.AddDays(1);
            }

            return weeks;
        }
    }
    internal class CalendarWeekStructure
    {
        public int WeekOfMonth { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
