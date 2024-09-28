#nullable disable

using GatewayManager.Domain.Abstract;
using System.ComponentModel.DataAnnotations;

namespace GatewayManager.Domain.Entities
{
    public class Gateway: BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string SerialNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string IPv4Address { get; set; }

        public virtual ICollection<PeripheralDevice> PeripheralDevices { get; set; }
    }

}
