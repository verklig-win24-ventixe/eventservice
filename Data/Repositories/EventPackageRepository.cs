using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class EventPackageRepository(DataContext context) : BaseRepository<EventPackageEntity>(context), IEventPackageRepository { }