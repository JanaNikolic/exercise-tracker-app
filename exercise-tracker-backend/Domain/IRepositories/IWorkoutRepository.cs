using Domain.DomainModels;

namespace Domain.IRepositories
{
    public interface IWorkoutRepository
    {
        Task<WorkoutDomainModel> InsertAsync(WorkoutCreationDomainModel workout);
        Task<WorkoutDomainModel> GetByIdAsync(long id);
        Task<IEnumerable<WorkoutDomainModel>> GetByUserIdAsync(long userId);
    }
}
