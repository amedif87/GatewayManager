#nullable disable

using GatewayManager.Infrastructure.Persistance.Database;
using GatewayManager.Infrastructure.Persistance.Shared;
using GatewayManager.Domain.Entities;
using GatewayManager.Domain.IRepositories;

namespace GatewayManager.Infrastructure.Persistance
{
    public class GatewayRepository : GenericRepository<Gateway>, IGatewayRepository
    {
        private readonly GatewayContext _dbContext;
        public GatewayRepository(GatewayContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
