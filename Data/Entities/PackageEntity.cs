using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class PackageEntity
{
  [Key]
  public int Id { get; set; }
  public string Title { get; set; } = null!;
  public string? SeatingArrangement { get; set; }
  public string? Placement { get; set; }
  public string? Currency { get; set; }
}