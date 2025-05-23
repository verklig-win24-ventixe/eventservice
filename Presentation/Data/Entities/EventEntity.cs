using System.ComponentModel.DataAnnotations;

namespace Presentation.Data.Entities;

public class EventEntity
{
  [Key]
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string? Image { get; set; }
  public string Title { get; set; } = null!;
  public string Date { get; set; } = null!;
  public string Destination { get; set; } = null!;
  public decimal Price { get; set; }
  public string Description { get; set; } = null!;
}