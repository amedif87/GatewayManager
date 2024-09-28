#nullable disable

using GatewayManager.Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GatewayManager.Domain.Entities
{
    public class PeripheralDevice: BaseEntity
    {
        public int UID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Vendor { get; set; }

        public DeviceStatus Status { get; set; }

        public long GatewayId { get; set; }
        [ForeignKey("GatewayId")]
        public virtual Gateway Gateway { get; set; }
    }
    public enum DeviceStatus
    {
        online = 1,
        offline = 0
    }

}
