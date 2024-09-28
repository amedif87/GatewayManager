using GatewayManager.Domain.DTOs;
using GatewayManager.Domain.IServices.Shared;
using GatewayManager.Domain.Pagination;
using GatewayManager.Domain.Pagination.Filters;

namespace GatewayManager.Domain.IServices
{
    public interface IPeripheralDeviceService : ICrudService<PeripheralDeviceDTO>
    {
        Task<PaginationDTO<PeripheralDeviceDTO>> GetPage(PeripheralDeviceFilter conditions);
    }
}
