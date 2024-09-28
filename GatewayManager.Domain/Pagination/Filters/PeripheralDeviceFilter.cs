#nullable disable

using GatewayManager.Domain.DTOs;

namespace GatewayManager.Domain.Pagination.Filters
{
    public class PeripheralDeviceFilter : PageInfo
    {
        public int? FilterByUID { get; set; }
        public string FilterByVendor { get; set; }
        public string  FilterByStatus { get; set; }

    }
}
