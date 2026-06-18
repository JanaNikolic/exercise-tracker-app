using Domain.DomainModels;

namespace Domain.IServices
{
    public interface IAuthService
    {
        string CreateToken(UserDomainModel user);
    }
}