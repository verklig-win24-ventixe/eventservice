using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Data.Contexts;
using Infrastructure.Interfaces;
using Data.Interfaces;

namespace Infrastructure.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
  private readonly IEventRepository _eventRepository = eventRepository;

  public Task<IEnumerable<EventEntity>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<EventEntity?> GetAsync(string id)
  {
    throw new NotImplementedException();
  }
}
