using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class AddPointClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Point = table.Column<Point>(type: "geography", nullable: false),
                    RegisterTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_Points_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Points_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1079a8d2-185e-4c94-90a4-ed4024f077ca", "AQAAAAIAAYagAAAAEAgfzDQXCWp/J8TfvEJDcFUjpAM0yO5O1+laMIJPY6qdaLOcU3QlWceKfZ/D1q5uhA==", "56150b00-b406-468a-ad98-8f2c0a18acab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1178274b-be0e-4fa2-9d5d-70b71ff4fb1a", "AQAAAAIAAYagAAAAEOiqjVKDdKzxFnB4puWxpILir+TPFIw2BlbZeDFIp1TusBfPoH8236Qyv7kiTPLwkQ==", "cec7d101-b7f9-4726-a3b1-f76047819cde" });

            migrationBuilder.CreateIndex(
                name: "IX_Points_DriverId",
                table: "Points",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_VehicleId",
                table: "Points",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26b85487-e378-4c0c-94d5-140fa07f5a8d", "AQAAAAIAAYagAAAAEC5ECwu8zRevAfZyKn5dk4CeV4dq2xBXTLCJrPwa9ENdtrqhj7U7ZlewBYmjcEbPuQ==", "bfcbb7e9-bd7e-4254-b1f9-6c2c4a883c01" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d1575eda-abd3-4ad8-95bf-5c81d8bbdcf3", "AQAAAAIAAYagAAAAEGACcI/1cNBFJ9EEnibfolLxbLSJBXVN8Ja9ncdYfyhGH8YlT3nUSKEgdqvcZZHkvA==", "9fe4e1f3-7c43-4b9a-b28f-b14a746e0347" });
        }
    }
}
