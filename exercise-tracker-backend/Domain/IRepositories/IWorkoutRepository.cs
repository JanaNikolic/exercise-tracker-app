using Domain.DomainModels;

namespace Domain.IRepositories
{
    public interface IWorkoutRepository
    {
        Task<WorkoutDomainModel> InsertAsync(WorkoutCreationDomainModel workout);
        Task<WorkoutDomainModel> GetByIdAsync(long id);
        Task<IEnumerable<WorkoutDomainModel>> GetByUserIdAsync(long userId);
        Task<IEnumerable<WorkoutDomainModel>> GetWorkoutsInDateRangeAsync(long userId, DateTime startDate, DateTime endDate);
        Task<(IEnumerable<WorkoutDomainModel> Items, int TotalCount)> GetPagedUserWorkoutsAsync(long userId, int page, int size);
        Task<bool> DeleteAsync(long id, long userId);
    }
}
