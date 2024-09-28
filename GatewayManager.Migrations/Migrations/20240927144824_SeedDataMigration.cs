using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GatewayManager.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Gateways",
                columns: new[] { "Id", "SerialNumber", "Name", "IPv4Address", "DateCreated", "DateUpdated" },
                values: new object[,]
                {
                    { 1L, "SN001", "Gateway 1", "192.168.1.1", DateTime.UtcNow, DateTime.UtcNow },
                    { 2L, "SN002", "Gateway 2", "192.168.1.2", DateTime.UtcNow, DateTime.UtcNow },
                    { 3L, "SN003", "Gateway 3", "192.168.1.3", DateTime.UtcNow, DateTime.UtcNow },
                    { 4L, "SN004", "Gateway 4", "192.168.1.4", DateTime.UtcNow, DateTime.UtcNow }
                }
            );
      
            migrationBuilder.InsertData(
                table: "PeripheralDevices",
                columns: new[] { "UID", "Vendor", "Status", "GatewayId", "DateCreated", "DateUpdated" },
                values: new object[,]
                {
                    { 1001, "Vendor A", "online", 1L, DateTime.UtcNow, DateTime.UtcNow },
                    { 1002, "Vendor A", "offline", 1L, DateTime.UtcNow, DateTime.UtcNow },
                    { 1003, "Vendor A", "online", 1L, DateTime.UtcNow, DateTime.UtcNow },
                    { 2001, "Vendor B", "online", 2L, DateTime.UtcNow, DateTime.UtcNow },
                    { 2002, "Vendor B", "online", 2L, DateTime.UtcNow, DateTime.UtcNow },
                    { 2003, "Vendor B", "offline", 2L, DateTime.UtcNow, DateTime.UtcNow },
                    { 2004, "Vendor B", "online", 2L, DateTime.UtcNow, DateTime.UtcNow },
                    { 2005, "Vendor B", "offline", 2L, DateTime.UtcNow, DateTime.UtcNow },
                    { 3001, "Vendor C", "online", 3L, DateTime.UtcNow, DateTime.UtcNow },
                    { 3002, "Vendor C", "online", 3L, DateTime.UtcNow, DateTime.UtcNow },
                    { 3003, "Vendor C", "offline", 3L, DateTime.UtcNow, DateTime.UtcNow },
                    { 3004, "Vendor C", "online", 3L, DateTime.UtcNow, DateTime.UtcNow },
                    { 3005, "Vendor C", "online", 3L, DateTime.UtcNow, DateTime.UtcNow },
                    { 3006, "Vendor C", "offline", 3L, DateTime.UtcNow, DateTime.UtcNow },
                    { 3007, "Vendor C", "online", 3L, DateTime.UtcNow, DateTime.UtcNow },
                    { 4001, "Vendor D", "online", 4L, DateTime.UtcNow, DateTime.UtcNow },
                    { 4002, "Vendor D", "online", 4L, DateTime.UtcNow, DateTime.UtcNow },
                    { 4003, "Vendor D", "offline", 4L, DateTime.UtcNow, DateTime.UtcNow },
                    { 4004, "Vendor D", "online", 4L, DateTime.UtcNow, DateTime.UtcNow },
                    { 4005, "Vendor D", "offline", 4L, DateTime.UtcNow, DateTime.UtcNow },
                    { 4006, "Vendor D", "online", 4L, DateTime.UtcNow, DateTime.UtcNow },
                    { 4007, "Vendor D", "offline", 4L, DateTime.UtcNow, DateTime.UtcNow },
                    { 4008, "Vendor D", "online", 4L, DateTime.UtcNow, DateTime.UtcNow },
                    { 4009, "Vendor D", "online", 4L, DateTime.UtcNow, DateTime.UtcNow },
                    { 4010, "Vendor D", "offline", 4L, DateTime.UtcNow, DateTime.UtcNow }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
