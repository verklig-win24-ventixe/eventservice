using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class EventEntity
{
  [Key]
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string? Image { get; set; }
  public string Title { get; set; } = null!;
  public DateTime Date { get; set; }
  public DateTime Time { get; set; }
  public string Location { get; set; } = null!;
  public decimal Price { get; set; }
  public string Description { get; set; } = null!;

  public ICollection<EventPackageEntity> Packages { get; set; } = [];
}