using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class PackageRepository(DataContext context) : BaseRepository<PackageEntity>(context), IPackageRepository { }