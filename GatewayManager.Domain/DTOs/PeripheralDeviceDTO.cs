#nullable disable

using System.ComponentModel.DataAnnotations;

namespace GatewayManager.Domain.DTOs
{
    public class PeripheralDeviceDTO
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "UID is required and must be greater than zero.")]
        public int UID { get; set; }


        [Required(ErrorMessage = "Vendor is required.")]
        [MaxLength(100, ErrorMessage = "The Vendor cannot exceed 100 characters.")]
        public string Vendor { get; set; }


        [Range(1, long.MaxValue, ErrorMessage = "GatewayId is required and must be greater than zero.")]
        public long GatewayId { get; set; }

        public string GatewayName { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }

}
