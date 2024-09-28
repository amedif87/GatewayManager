#nullable disable

using GatewayManager.Infrastructure.Persistance.Database;
using GatewayManager.Infrastructure.Persistance.Shared;
using GatewayManager.Domain.Entities;
using GatewayManager.Domain.IRepositories;

namespace GatewayManager.Infrastructure.Persistance
{
    public class PeripheralDeviceRepository : GenericRepository<PeripheralDevice>, IPeripheralDeviceRepository
    {
        private readonly GatewayContext _dbContext;
        public PeripheralDeviceRepository(GatewayContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
