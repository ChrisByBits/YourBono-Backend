using AlquilaFacilPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using AlquilaFacilPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using YourBonoPlatform.Bonds.Domain.Model.Entities;
using YourBonoPlatform.Bonds.Domain.Repositories;

namespace YourBonoPlatform.Bonds.Infrastructure.Persistence.EFC.Repositories;

public class InterestTypeRepository(AppDbContext context): BaseRepository<InterestType>(context), IInterestTypeRepository
{
    
}