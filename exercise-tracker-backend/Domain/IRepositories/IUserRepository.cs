using Domain.DomainModels;

namespace Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<UserDomainModel> GetByIdAsync(long id);
        Task<UserDomainModel> InsertAsync(UserCreationDomainModel user);
        Task<UserDomainModel> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
    }
}