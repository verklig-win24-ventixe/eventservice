using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController(EventService eventService) : ControllerBase
{
  private readonly EventService _eventService = eventService;

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var result = await _eventService.GetAllAsync();
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> Get(string id)
  {
    var result = await _eventService.GetAsync(id);
    return result != null ? Ok(result) : NotFound();
  }
}