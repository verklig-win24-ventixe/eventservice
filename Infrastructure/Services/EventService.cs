using Data.Entities;
using Infrastructure.Interfaces;
using Data.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services;

public class EventService(IEventRepository eventRepository, IPackageRepository packageRepository, IEventPackageRepository eventPackageRepository) : IEventService
{
  private readonly IEventRepository _eventRepository = eventRepository;
  private readonly IPackageRepository _packageRepository = packageRepository;
  private readonly IEventPackageRepository _eventPackageRepository = eventPackageRepository;

  public async Task<EventResult> CreateEventAsync(CreateEventRequest request)
  {
    try
    {
      var eventEntity = new EventEntity
      {
        Image = request.Image,
        Title = request.Title,
        EventDate = request.EventDate,
        Location = request.Location,
        Description = request.Description
      };

      var eventResult = await _eventRepository.AddAsync(eventEntity);
      if (!eventResult.Success)
      {
        return new EventResult { Success = false, Error = eventResult.Error };
      }

      foreach (var package in request.Packages)
      {
        var packageEntity = new PackageEntity
        {
          Title = package.Title,
          SeatingArrangement = package.SeatingArrangement,
          Placement = package.Placement,
          Price = package.Price,
          Currency = package.Currency
        };

        var packageResult = await _packageRepository.AddAsync(packageEntity);
        if (!packageResult.Success)
        {
          continue;
        }

        var eventPackageEntity = new EventPackageEntity
        {
          EventId = eventEntity.Id,
          PackageId = packageEntity.Id
        };

        await _eventPackageRepository.AddAsync(eventPackageEntity);
      }

      return new EventResult { Success = true };
    }
    catch (Exception ex)
    {
      return new EventResult { Success = false, Error = ex.Message };
    }
  }

  public async Task<EventResult<IEnumerable<Event>>> GetAllEventsAsync()
  {
    var result = await _eventRepository.GetAllAsync();
    var events = result.Result?.Select(entity => new Event
    {
      Id = entity.Id,
      Image = entity.Image,
      Title = entity.Title,
      EventDate = entity.EventDate,
      Location = entity.Location,
      Description = entity.Description,
      Packages = [.. entity.Packages.Select(p => new Package
      {
        Id = p.Package.Id,
        Title = p.Package.Title,
        SeatingArrangement = p.Package.SeatingArrangement!,
        Placement = p.Package.Placement,
        Price = p.Package.Price,
        Currency = p.Package.Currency!
      })]
    });

    return new EventResult<IEnumerable<Event>> { Success = true, Result = events };
  }

  public async Task<EventResult<Event?>> GetEventAsync(string id)
  {
    var result = await _eventRepository.GetAsync(x => x.Id == id);
    if (result.Success && result.Result != null)
    {
      var currentEvent = new Event
      {
        Id = result.Result.Id,
        Image = result.Result.Image,
        Title = result.Result.Title,
        EventDate = result.Result.EventDate,
        Location = result.Result.Location,
        Description = result.Result.Description,
        Packages = [.. result.Result.Packages.Select(p => new Package
        {
          Id = p.Package.Id,
          Title = p.Package.Title,
          SeatingArrangement = p.Package.SeatingArrangement!,
          Placement = p.Package.Placement,
          Price = p.Package.Price,
          Currency = p.Package.Currency!
        })]
      };

      return new EventResult<Event?> { Success = true, Result = currentEvent };
    }

    return new EventResult<Event?> { Success = false, Error = "Event was not found." };
  }
}
