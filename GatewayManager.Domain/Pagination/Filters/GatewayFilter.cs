#nullable disable

namespace GatewayManager.Domain.Pagination.Filters
{
    public class GatewayFilter : PageInfo
    {
        public string FilterBySerialNumber { get; set; }
        public string FilterByName { get; set; }
        public string FilterByIPv4Address { get; set; }

    }
}
