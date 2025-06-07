using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class EventRepository(DataContext context) : BaseRepository<EventEntity>(context), IEventRepository
{
  public override async Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync()
  {
    try
    {
      var entities = await _db.Include(x => x.Packages).ThenInclude(p => p.Package).ToListAsync();

      return new RepositoryResult<IEnumerable<EventEntity>> { Success = true, Result = entities };
    }
    catch (Exception ex)
    {
      return new RepositoryResult<IEnumerable<EventEntity>> { Success = false, Error = ex.Message };
    }
  }

  public override async Task<RepositoryResult<EventEntity?>> GetAsync(Expression<Func<EventEntity, bool>> expression)
  {
    try
    {
      var entity = await _db.Include(x => x.Packages).ThenInclude(p => p.Package).FirstOrDefaultAsync(expression) ?? throw new Exception("Entity was not found.");

      return new RepositoryResult<EventEntity?> { Success = true, Result = entity };
    }
    catch (Exception ex)
    {
      return new RepositoryResult<EventEntity?> { Success = false, Error = ex.Message };
    }
  }
}