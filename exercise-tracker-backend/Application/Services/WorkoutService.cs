using AutoMapper;
using Domain.DomainModels;
using Domain.IRepositories;
using Domain.IServices;

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
    }
}
