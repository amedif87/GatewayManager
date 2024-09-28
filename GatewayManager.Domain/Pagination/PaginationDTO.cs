namespace GatewayManager.Domain.Pagination
{
    public class PaginationDTO<DTO> where DTO : class, new()
    {
        public int TotalCount { get; set; }
        public IEnumerable<DTO>? Items { get; set; }
    }
}
