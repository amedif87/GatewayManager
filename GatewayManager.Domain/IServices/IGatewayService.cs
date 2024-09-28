using GatewayManager.Domain.DTOs;
using GatewayManager.Domain.IServices.Shared;
using GatewayManager.Domain.Pagination;
using GatewayManager.Domain.Pagination.Filters;

namespace GatewayManager.Domain.IServices
{
    public interface IGatewayService : ICrudService<GatewayDTO>
    {
        Task<PaginationDTO<GatewayDTO>> GetPage(GatewayFilter conditions);
    }
}
