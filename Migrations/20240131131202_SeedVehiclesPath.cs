using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class SeedVehiclesPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Points_Drivers_DriverId",
                table: "Points");

            migrationBuilder.DropForeignKey(
                name: "FK_Points_Vehicles_VehicleId",
                table: "Points");

            migrationBuilder.DropIndex(
                name: "IX_Points_DriverId",
                table: "Points");

            migrationBuilder.DropIndex(
                name: "IX_Points_VehicleId",
                table: "Points");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f58b5106-1d5c-49f4-8416-e66d62769794", "AQAAAAIAAYagAAAAEF5twEtYO7+mwyDJAEG+VjR/JIJsSw5/LVSTjDTHSVE27mJT/cyJ3ygSGVvvf6nYPQ==", "a845c569-bd58-422f-ac7b-2f274b8a9347" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54e50f68-8d29-42cc-8bd8-36dcb4f5762c", "AQAAAAIAAYagAAAAEOz3LKwZoRYxx/fMjskCu844n1B35YZSJcYA+rXnQtb0MS2u/zCXvHTbIIO+IqvOsQ==", "834a8a4d-baf9-4dff-a99c-add5ccc93261" });

            migrationBuilder.InsertData(
                table: "Points",
                columns: new[] { "UUID", "DriverId", "Point", "RegisterTime", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("34451e00-16e8-401d-9a64-812bc5c6d72f"), 1, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (7 8)"), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("5f0814c7-a66f-4bac-894c-227091ad8c53"), 1, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (3 4)"), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("95958216-3829-489a-a518-776d3349a957"), 1, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (5 6)"), new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("b2768ea2-678a-4520-a469-48dd92db5c41"), 1, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (1 2)"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Points",
                keyColumn: "UUID",
                keyValue: new Guid("34451e00-16e8-401d-9a64-812bc5c6d72f"));

            migrationBuilder.DeleteData(
                table: "Points",
                keyColumn: "UUID",
                keyValue: new Guid("5f0814c7-a66f-4bac-894c-227091ad8c53"));

            migrationBuilder.DeleteData(
                table: "Points",
                keyColumn: "UUID",
                keyValue: new Guid("95958216-3829-489a-a518-776d3349a957"));

            migrationBuilder.DeleteData(
                table: "Points",
                keyColumn: "UUID",
                keyValue: new Guid("b2768ea2-678a-4520-a469-48dd92db5c41"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Points_Drivers_DriverId",
                table: "Points",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Points_Vehicles_VehicleId",
                table: "Points",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
