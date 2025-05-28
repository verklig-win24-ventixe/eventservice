using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IEventService
{
  Task<EventResult> CreateEventAsync(CreateEventRequest request);
  Task<EventResult<IEnumerable<Event>>> GetAllEventsAsync();
  Task<EventResult<Event?>> GetEventAsync(string id);
}
