using AutoMapper;
using Domain.DomainModels;
using Domain.Exceptions;
using Domain.IRepositories;
using Infrastructure.DatabaseModels;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext timeSheetDbContext, IMapper mapper)
            : base(timeSheetDbContext)
        {
            _mapper = mapper;
        }

        public async Task<UserDomainModel> GetByIdAsync(long id)
        {
            var entity = await GetUserByIdAsync(id);

            return _mapper.Map<UserDomainModel>(entity);
        }

        public async Task<UserDomainModel> InsertAsync(UserCreationDomainModel user)
        {
            try
            {
                var entity = _mapper.Map<User>(user);

                var newUser = await AddAsync(entity);

                return _mapper.Map<UserDomainModel>(newUser.Entity);
            }
            catch (Exception ex)
            {
                throw new CreationException<User>();
            }
        }

        public async Task<UserDomainModel> GetByEmailAsync(string email)
        {
            var entity = await GetUserByEmailAsync(email);
            return _mapper.Map<UserDomainModel>(entity);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await AnyAsync(u => u.Email == email);
        }

        private async Task<User> GetUserByEmailAsync(string email)
        {
            var entity = await FirstOrDefaultAsync(u => u.Email.Equals(email));
            if (entity == null)
            {
                throw new NotFoundException<User>(email);
            }
            return entity;
        }

        private async Task<User> GetUserByIdAsync(long id)
        {
            try
            {
                var entity = await FirstOrDefaultAsync(c => c.Id == id);
                if (entity == null)
                {
                    throw new NotFoundException<User>(id);
                }
                return entity;
            }
            catch (Exception ex) when (ex is not NotFoundException<User>)
            {
                throw new RetrievalException<User>(id);
            }
        }
    }
}