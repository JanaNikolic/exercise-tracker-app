using Domain.DomainModels;

namespace Domain.IServices
{
    public interface IUserService
    {
        Task<UserDomainModel> InsertAsync(UserCreationDomainModel user);
        Task<UserDomainModel> GetByIdAsync(long id);
    }
}
