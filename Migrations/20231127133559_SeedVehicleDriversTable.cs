using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class SeedVehicleDriversTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverVehicle");

            migrationBuilder.CreateTable(
                name: "VehicleDriver",
                columns: table => new
                {
                    AssignedDriversId = table.Column<int>(type: "int", nullable: false),
                    AssignedCarsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleDriver", x => new { x.AssignedDriversId, x.AssignedCarsId });
                    table.ForeignKey(
                        name: "FK_VehicleDriver_Drivers_AssignedDriversId",
                        column: x => x.AssignedDriversId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleDriver_Vehicles_AssignedCarsId",
                        column: x => x.AssignedCarsId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VehicleDriver",
                columns: new[] { "AssignedCarsId", "AssignedDriversId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 1 },
                    { 1, 2 },
                    { 4, 2 },
                    { 2, 3 },
                    { 5, 3 },
                    { 2, 4 },
                    { 5, 4 },
                    { 6, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleDriver_AssignedCarsId",
                table: "VehicleDriver",
                column: "AssignedCarsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleDriver");

            migrationBuilder.CreateTable(
                name: "DriverVehicle",
                columns: table => new
                {
                    AssignedCarsId = table.Column<int>(type: "int", nullable: false),
                    AssignedDriversId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverVehicle", x => new { x.AssignedCarsId, x.AssignedDriversId });
                    table.ForeignKey(
                        name: "FK_DriverVehicle_Drivers_AssignedDriversId",
                        column: x => x.AssignedDriversId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverVehicle_Vehicles_AssignedCarsId",
                        column: x => x.AssignedCarsId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverVehicle_AssignedDriversId",
                table: "DriverVehicle",
                column: "AssignedDriversId");
        }
    }
}
