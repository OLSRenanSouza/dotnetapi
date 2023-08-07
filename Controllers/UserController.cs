using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    public UserController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
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
    //public IEnumerable<User> GetUsers()
    public string[] GetUsers()
    {
        _logger.LogInformation("GetUsers has been called.");
        return new string[] { "Renan", "Jessica" };
        /*return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();*/
    }
}