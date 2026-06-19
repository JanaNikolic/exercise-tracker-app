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
    [HttpGet("monthly-summary")]
    public async Task<IActionResult> GetMonthlySummary([FromQuery] int year, [FromQuery] int month)
    {
        if (month < 1 || month > 12 || year < 2000 || year > 2100)
        {
            return BadRequest(new { message = "Invalid month or year parameters specified." });
        }

        long? userId = User.GetUserId();
        if (userId == null) return Unauthorized();

        var domainSummaries = await _workoutService.GetMonthlyWeeklySummaryAsync(userId.Value, year, month);

        var apiResponse = _mapper.Map<IEnumerable<WeeklySummaryApiModel>>(domainSummaries);

        return Ok(apiResponse);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllWorkoutsAsync([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        long? userId = User.GetUserId();
        if (userId == null)
        {
            return Unauthorized(new { message = "Invalid user session." });
        }

        var domainWorkouts = await _workoutService.GetPagedUserWorkoutsAsync(userId.Value, page, size);

        var apiWorkoutModels = _mapper.Map<IEnumerable<WorkoutApiModel>>(domainWorkouts);

        var response = new PaginatedListApiModel<WorkoutApiModel>(apiWorkoutModels, page, size);

        return Ok(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync(long id)
    {
        long? userId = User.GetUserId();
        if (userId == null)
        {
            return Unauthorized(new { message = "Invalid user session." });
        }
        bool wasDeleted = await _workoutService.DeleteAsync(id, userId.Value);

        if (!wasDeleted)
        {
            return NotFound(new { message = $"Workout record with ID {id} was not found." });
        }

        return NoContent();
    }
}