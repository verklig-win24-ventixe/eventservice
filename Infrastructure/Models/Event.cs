namespace Infrastructure.Models;

public class Event
{
  public string Id { get; set; } = null!;
  public string? Image { get; set; }
  public string Title { get; set; } = null!;
  public DateTime EventDate { get; set; }
  public string Location { get; set; } = null!;
  public string Description { get; set; } = null!;

  public List<Package> Packages { get; set; } = [];
}