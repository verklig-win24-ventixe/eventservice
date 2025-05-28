using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;
using Infrastructure.Interfaces;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController(IEventService eventService) : ControllerBase
{
  private readonly IEventService _eventService = eventService;

  [HttpPost]
  public async Task<IActionResult> Create(CreateEventRequest request)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var result = await _eventService.CreateEventAsync(request);
    return result != null ? Ok(result) : StatusCode(500, result?.Error );
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var result = await _eventService.GetAllEventsAsync();
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> Get(string id)
  {
    var result = await _eventService.GetEventAsync(id);
    return result != null ? Ok(result) : NotFound();
  }
}