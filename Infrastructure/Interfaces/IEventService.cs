using Data.Entities;

namespace Infrastructure.Interfaces;

public interface IEventService
{
  Task<IEnumerable<EventEntity>> GetAllAsync();
  Task<EventEntity?> GetAsync(string id);
}
