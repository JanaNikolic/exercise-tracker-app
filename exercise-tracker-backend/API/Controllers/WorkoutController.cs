using API.APIModels;
using API.DTOs;
using API.Extensions;
using AutoMapper;
using Domain.DomainModels;
using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/workouts")]
[Authorize]
public class WorkoutController : ControllerBase
{
    private readonly IWorkoutService _workoutService;
    private readonly IMapper _mapper;

    public WorkoutController(IWorkoutService workoutService, IMapper mapper)
    {
        _workoutService = workoutService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> InsertWorkoutAsync([FromBody] WorkoutForCreationDto body)
    {
        long? userId = User.GetUserId();
        if (userId == null)
        {
            return Unauthorized(new { message = "Invalid user session." });
        }

        var workout = _mapper.Map<WorkoutCreationDomainModel>(body);

        workout.UserId = userId.Value;

        var savedWorkout = await _workoutService.InsertAsync(workout);
        var apiResponseModel = _mapper.Map<WorkoutApiModel>(savedWorkout);

        return CreatedAtAction(nameof(GetWorkoutByIdAsync), new { id = apiResponseModel.Id }, apiResponseModel);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkoutByIdAsync(long id)
    {
        var workout = await _workoutService.GetByIdAsync(id);
        var workoutApiModel = _mapper.Map<WorkoutApiModel>(workout);
        return Ok(workoutApiModel);
    }
}