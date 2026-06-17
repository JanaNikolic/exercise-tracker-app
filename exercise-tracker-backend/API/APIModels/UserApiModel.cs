using Domain.DomainModels;

namespace API.APIModels
{
    public class UserApiModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public UserApiModel(UserDomainModel user)
        {
            Id = user.Id;
            Name = user.Name;
        }

        public UserApiModel(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}