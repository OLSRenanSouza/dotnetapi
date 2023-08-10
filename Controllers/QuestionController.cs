using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DotnetAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly ILogger<QuestionController> _logger;
  private readonly DataContextEF _context;

  public QuestionController(ILogger<QuestionController> logger, DataContextEF context)
  {
      _logger = logger;
      _context = context;
  }
  [HttpPost("")]
  public async Task<ActionResult<Question>> CreateQuestion(CreateQuestionDTO questionDTO)
  {
    _logger.LogInformation("CreateQuestion has been called.");

    if (questionDTO == null)
    {
        return BadRequest("Question data is null.");
    }

    Question question = new()
    {
      Body = questionDTO.Body,
      Answer = questionDTO.Answer,
      Tip = questionDTO.Tip,
      CreatedAt = DateTime.UtcNow
    };
      
    if (_context.Questions != null)
    {
      _context.Questions.Add(question);
      // _constext.SaveChanges return de number of rows that were modified.
      if (await _context.SaveChangesAsync() > 0)
      {
        return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
      }
      throw new Exception("Error to Add this Question");
    }
    return StatusCode(500);

  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Question>> GetQuestion(int id)
  {
    _logger.LogInformation("GetQuestions has been called.");
    if (_context.Questions != null)
    {
      Question? question = await _context.Questions.SingleOrDefaultAsync(u => u.Id == id);

      return Ok(question);
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
  public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
  {
    _logger.LogInformation("GetQuestions has been called.");
    if (_context.Questions != null)
    {
      IEnumerable<Question?> questions = await _context.Questions.ToListAsync();
  
      return Ok(questions);
    }
    return StatusCode(500);
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult<Question>> PatchQuestion(int id,[FromBody] UpdateQuestionDTO updateQuestionDTO)
  {
    _logger.LogInformation("PatchQuestions has been called.");
    if (_context.Questions != null)
    {
      Question? question = await _context.Questions.SingleOrDefaultAsync(u => u.Id == id);
      if (question == null)
     if (question == null) {
        return NotFound("Question id: " + id + "not found");
      }
      if(updateQuestionDTO.Body != null) question.Body = updateQuestionDTO.Body;
      if(updateQuestionDTO.Answer != null) question.Answer = (char)updateQuestionDTO.Answer;
      if(updateQuestionDTO.Tip != null) question.Tip = updateQuestionDTO.Tip;
      question.LastUpdatedAt = DateTime.UtcNow;

      if (await _context.SaveChangesAsync() > 0)
      {
        return Ok(question);
      }
      throw new Exception("Error to update Question");

    }
    return StatusCode(500);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteQuestion(int id)
  {
    _logger.LogInformation("DeleteQuestion has been called.");
    if (_context.Questions != null)
    {
      Question? question = await _context.Questions.SingleOrDefaultAsync(u => u.Id == id);

      if (question == null) 
      {
        return NotFound("Question id: " + id + "not found");
      }

      _context.Questions.Remove(question);
      if( await _context.SaveChangesAsync() > 0) {
        return NoContent();
      };
      throw new Exception("Error to delete Question");

    }
    return StatusCode(500);
  }
}
