using Presentation.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
  public DbSet<EventEntity> Events { get; set; }
}