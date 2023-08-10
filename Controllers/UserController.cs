using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

  private readonly ILogger<UserController> _logger;
  private readonly DataContextEF _context;

  public UserController(ILogger<UserController> logger, DataContextEF context)
  {
      _logger = logger;
      _context = context;
  }
  [HttpPost("")]
  public async Task<ActionResult<User>> CreateUser(CreateUserDTO userDTO)
  {
    _logger.LogInformation("CreateUser has been called.");

    if (userDTO == null || userDTO.Email == null)
    {
        return BadRequest("User data or email is null.");
    }

    User user = new()

    {
      Name = userDTO.Name,
      Email = userDTO.Email,
      CreatedAt = DateTime.UtcNow
    };
      
    if (_context.Users != null)
    {
      _context.Users.Add(user);
      // _constext.SaveChanges return de number of rows that were modified.
      if (await _context.SaveChangesAsync() > 0)
      {
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
      }
      throw new Exception("Error to Add this User");
    }
    return StatusCode(500);

  }

  [HttpGet("{id}")]
  public async Task<ActionResult<User>> GetUser(int id)
  {
    _logger.LogInformation("GetUsers has been called.");
    if (_context.Users != null)
    {
      User? user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

      return Ok(user);
    }
    return StatusCode(500);
    /*return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
      Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();*/
  }

  [HttpGet("")]
  public async Task<ActionResult<IEnumerable<User>>> GetUsers()
  {
    _logger.LogInformation("GetUsers has been called.");
    if (_context.Users != null)
    {
      IEnumerable<User?> users = await _context.Users.ToListAsync();
  
      return Ok(users);
    }
    return StatusCode(500);
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult<User>> PatchUser(int id,[FromBody] UpdateUserDTO updateUserDTO)
  {
    _logger.LogInformation("PatchUsers has been called.");
    if (_context.Users != null)
    {
      User? user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
      if (user == null)
     if (user == null) {
        return NotFound("User id: " + id + "not found");
      }
      if(updateUserDTO.Name != null) user.Name = updateUserDTO.Name;
      if(updateUserDTO.Email != null) user.Email = updateUserDTO.Email;

      if (await _context.SaveChangesAsync() > 0)
      {
        return Ok(user);
      }
      throw new Exception("Error to update User");

    }
    return StatusCode(500);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteUser(int id)
  {
    _logger.LogInformation("DeleteUser has been called.");
    if (_context.Users != null)
    {
      User? user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

      if (user == null) 
      {
        return NotFound("User id: " + id + "not found");
      }

      _context.Users.Remove(user);
      if( await _context.SaveChangesAsync() > 0) {
        return NoContent();
      };
      throw new Exception("Error to delete User");

    }
    return StatusCode(500);
  }


}