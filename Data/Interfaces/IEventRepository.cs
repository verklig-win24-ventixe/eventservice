using System.Linq.Expressions;
using Data.Entities;
using Data.Models;

namespace Data.Interfaces;

public interface IEventRepository
{
  Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync(EventEntity entity);
  Task<RepositoryResult<EventEntity?>> GetAsync(Expression<Func<EventEntity, bool>> expression);
}
