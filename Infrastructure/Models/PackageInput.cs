namespace Infrastructure.Models;

public class PackageInput
{
  public string Title { get; set; } = null!;
  public string SeatingArrangement { get; set; } = null!;
  public string? Placement { get; set; }
  public int Price { get; set; }
  public string Currency { get; set; } = "$";
}