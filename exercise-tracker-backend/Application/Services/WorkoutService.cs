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

                if (!workouts.Any()) return Enumerable.Empty<WeeklySummaryDomainModel>();

                var calendar = CultureInfo.CurrentCulture.Calendar;

                var weeklySummaries = workouts
                    .GroupBy(w => calendar.GetWeekOfYear(w.TrainingDateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
                    .Select((group, index) =>
                    {
                        var orderedWorkouts = group.OrderBy(w => w.TrainingDateTime).ToList();

                        return new WeeklySummaryDomainModel
                        {
                            WeekNumber = index + 1,
                            WeekStart = orderedWorkouts.First().TrainingDateTime.Date,
                            WeekEnd = orderedWorkouts.Last().TrainingDateTime.Date,

                            TotalDurationInMinutes = group.Sum(w => w.DurationInMinutes),
                            TotalNumberOfTrainings = group.Count(),

                            AverageIntensity = Math.Round(group.Average(w => w.IntensityLevel), 1),
                            AverageFatigue = Math.Round(group.Average(w => w.FatigueLevel), 1)
                        };
                    })
                    .OrderBy(w => w.WeekNumber)
                    .ToList();

                return weeklySummaries;
            }
        public async Task<IEnumerable<WorkoutDomainModel>> GetPagedUserWorkoutsAsync(long userId, int page, int size)
        {
            if (page < 1) page = 1;
            if (size < 1 || size > 100) size = 10;

            return await _workoutRepository.GetPagedUserWorkoutsAsync(userId, page, size);
        }
        public async Task<bool> DeleteAsync(long id, long userId)
        {
            return await _workoutRepository.DeleteAsync(id, userId);
        }
    }
}
