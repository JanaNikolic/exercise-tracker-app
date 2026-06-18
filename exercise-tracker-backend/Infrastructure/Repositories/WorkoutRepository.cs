using AutoMapper;
using Domain.DomainModels;
using Domain.Exceptions;
using Domain.IRepositories;
using Infrastructure.DatabaseModels;

namespace Infrastructure.Repositories
{
    public class WorkoutRepository : BaseRepository<Workout>, IWorkoutRepository
    {
        private readonly IMapper _mapper;

        public WorkoutRepository(AppDbContext dbContext, IMapper mapper)
            : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task<WorkoutDomainModel> InsertAsync(WorkoutCreationDomainModel workout)
        {
            try
            {
                var entity = _mapper.Map<Workout>(workout);

                var newWorkout = await AddAsync(entity);

                return _mapper.Map<WorkoutDomainModel>(newWorkout.Entity);
            }
            catch (Exception ex)
            {
                throw new CreationException<Workout>();
            }
        }

        public async Task<WorkoutDomainModel> GetByIdAsync(long id)
        {
            var entity = await GetWorkoutByIdAsync(id);

            return _mapper.Map<WorkoutDomainModel>(entity);
        }

        public async Task<IEnumerable<WorkoutDomainModel>> GetByUserIdAsync(long userId)
        {
            try
            {
                var result = await FindAllWhereAsync(w => w.UserId == userId);
                return _mapper.Map<IEnumerable<WorkoutDomainModel>>(result);
            }
            catch (Exception ex)
            {
                throw new RetrievalException<Workout>(userId);
            }
        }

        private async Task<Workout> GetWorkoutByIdAsync(long id)
        {
            try
            {
                var entity = await FirstOrDefaultAsync(w => w.Id == id);
                if (entity == null)
                {
                    throw new NotFoundException<Workout>(id);
                }
                return entity;
            }
            catch (Exception ex) when (ex is not NotFoundException<Workout>)
            {
                throw new RetrievalException<Workout>(id);
            }
        }
    }
}
