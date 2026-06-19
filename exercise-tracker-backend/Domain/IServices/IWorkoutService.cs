using Domain.DomainModels;

namespace Domain.IServices
{
    public interface IWorkoutService
    {
        Task<WorkoutDomainModel> InsertAsync(WorkoutCreationDomainModel workout);
        Task<WorkoutDomainModel> GetByIdAsync(long id);
        Task<IEnumerable<WorkoutDomainModel>> GetUserWorkoutsAsync(long userId);
        Task<IEnumerable<WeeklySummaryDomainModel>> GetMonthlyWeeklySummaryAsync(long userId, int year, int month);
        Task<IEnumerable<WorkoutDomainModel>> GetPagedUserWorkoutsAsync(long userId, int page, int size);
        Task<bool> DeleteAsync(long id, long userId);
    }
}
