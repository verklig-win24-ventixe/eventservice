namespace Infrastructure.Models;

public class CreateEventRequest
{
  public string? Image { get; set; }
  public string Title { get; set; } = null!;
  public DateTime EventDate { get; set; }
  public string Location { get; set; } = null!;
  public string Description { get; set; } = null!;

  public List<PackageInput> Packages { get; set; } = [];
}