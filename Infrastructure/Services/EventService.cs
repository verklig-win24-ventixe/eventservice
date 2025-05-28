using Data.Entities;
using Infrastructure.Interfaces;
using Data.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
  private readonly IEventRepository _eventRepository = eventRepository;

  public async Task<EventResult> CreateEventAsync(CreateEventRequest request)
  {
    try
    {
      var eventEntity = new EventEntity
      {
        Image = request.Image,
        Title = request.Title,
        EventDate = request.EventDate,
        Location = request.Location,
        Description = request.Description
      };

      var result = await _eventRepository.AddAsync(eventEntity);

      return result.Success
        ? new EventResult { Success = true }
        : new EventResult { Success = false, Error = result.Error };
    }
    catch (Exception ex)
    {
      return new EventResult { Success = false, Error = ex.Message };
    }
  }

  public async Task<EventResult<IEnumerable<Event>>> GetAllEventsAsync()
  {
    var result = await _eventRepository.GetAllAsync();
    var events = result.Result?.Select(entity => new Event
    {
      Id = entity.Id,
      Image = entity.Image,
      Title = entity.Title,
      EventDate = entity.EventDate,
      Location = entity.Location,
      Description = entity.Description
    }
    );

    return new EventResult<IEnumerable<Event>> { Success = true, Result = events };
  }

  public async Task<EventResult<Event?>> GetEventAsync(string id)
  {
    var result = await _eventRepository.GetAsync(x => x.Id == id);
    if (result.Success && result.Result != null)
    {
      var currentEvent = new Event
      {
        Id = result.Result.Id,
        Image = result.Result.Image,
        Title = result.Result.Title,
        EventDate = result.Result.EventDate,
        Location = result.Result.Location,
        Description = result.Result.Description
      };

      return new EventResult<Event?> { Success = true, Result = currentEvent };
    }

    return new EventResult<Event?> { Success = false, Error = "Event was not found." };
  }
}
