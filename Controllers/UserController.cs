using DotnetAPI.Data;
using DotnetAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly DataContextEF _context;

    public UserController(ILogger<WeatherForecastController> logger, DataContextEF context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(CreateUserDTO userDTO)
    {
        _logger.LogInformation("CreateUser has been called.");

        if (userDTO == null)
        {
            return BadRequest("User data is null.");
        }

        User user = new()

        {
            Name = userDTO.Name,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpGet("/{id}")]
    //public IEnumerable<User> GetUsers()
    public string GetUser(string id)
    {
        _logger.LogInformation("GetUsers has been called.");
        var users = new string[] { "Renan", "Jessica" };
        return users[int.Parse(id)];
        /*return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();*/
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        _logger.LogInformation("GetUsers has been called.");
        
        var users = await _context.Users.ToListAsync();
        
        return Ok(users);
    }
}