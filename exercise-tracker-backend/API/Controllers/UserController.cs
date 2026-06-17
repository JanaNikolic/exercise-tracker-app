using API.APIModels;
using API.DTOs;
using AutoMapper;
using Domain.DomainModels;
using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserForCreationDto body)
        {
            var dto = _mapper.Map<UserCreationDomainModel>(body);
            var user = await _userService.InsertAsync(dto);
            var userApiModel = new UserApiModel(user);
            return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, userApiModel);
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetUserAsync(long id)
        {
            var user = await _userService.GetByIdAsync(id);
            var userApiModel = _mapper.Map<UserDetailsApiModel>(user);
            return Ok(userApiModel);
        }
    }
}
