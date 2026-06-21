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
        public async Task<IEnumerable<WorkoutDomainModel>> GetWorkoutsInDateRangeAsync(long userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var entities = await FindAllWhereAsync(w => w.UserId == userId && 
                                                            w.TrainingDateTime >= startDate && 
                                                            w.TrainingDateTime <= endDate);

                return _mapper.Map<IEnumerable<WorkoutDomainModel>>(entities);
            }
            catch (Exception ex)
            {
                throw new RetrievalException<Workout>();
            }
        }
        public async Task<(IEnumerable<WorkoutDomainModel> Items, int TotalCount)> GetPagedUserWorkoutsAsync(long userId, int page, int size)
        {
            try
            {
                var entities = await GetPagedListAsync(
                    filter: w => w.UserId == userId,
                    page: page,
                    size: size,
                    true,
                    orderBy: query => query.OrderByDescending(w => w.TrainingDateTime)
                );

                int totalCount = await CountAsync(w => w.UserId == userId);

                var mappedItems = _mapper.Map<IEnumerable<WorkoutDomainModel>>(entities);

                return (mappedItems, totalCount);
            }
            catch (Exception ex)
            {
                throw new RetrievalException<Workout>();
            }
        }
        public async Task<bool> DeleteAsync(long id, long userId)
        {
            try
            {
                var entity = await FirstOrDefaultAsync(w => w.Id == id);

                if (entity == null || entity.UserId != userId)
                {
                    return false;
                }

                await Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                throw new DeletionException<Workout>(id);
            }
        }
    }
}
