using Domain.DomainModels;

namespace Domain.IServices
{
    public interface IWorkoutService
    {
        Task<WorkoutDomainModel> InsertAsync(WorkoutCreationDomainModel workout);
        Task<WorkoutDomainModel> GetByIdAsync(long id);
        Task<IEnumerable<WorkoutDomainModel>> GetUserWorkoutsAsync(long userId);
    }
}
