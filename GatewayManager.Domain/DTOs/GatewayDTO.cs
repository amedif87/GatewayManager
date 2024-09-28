#nullable disable

using System.ComponentModel.DataAnnotations;

namespace GatewayManager.Domain.DTOs
{
    public class GatewayDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "SerialNumber is required.")]
        [MaxLength(100, ErrorMessage = "The SerialNumber cannot exceed 100 characters.")]
        public string SerialNumber { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "The Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "IPv4Address is required.")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "IPv4 address is invalid.")]
        public string IPv4Address { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public List<PeripheralDeviceDTO> PeripheralDevices { get; set; }
    }

}
