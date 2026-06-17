using AutoMapper;
using Domain.DomainModels;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.IServices;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IMapper mapper, IAuthService authService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<UserDomainModel> GetByIdAsync(long id) => await _userRepository.GetByIdAsync(id);

        public async Task<UserDomainModel> InsertAsync(UserCreationDomainModel user)
        {
            bool emailExists = await _userRepository.ExistsByEmailAsync(user.Email);
            if (emailExists)
            {
                throw new AlreadyExistsException("This email address is already registered.");
            }
            string securePasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = securePasswordHash;

            var newUser = await _userRepository.InsertAsync(user);
            return newUser;
        }

        public async Task<AuthDomainModel> LoginAsync(LoginDomainModel request)
        {
            UserDomainModel user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = _authService.CreateToken(user);

            return new AuthDomainModel { Token = token };
        }
    }
}
