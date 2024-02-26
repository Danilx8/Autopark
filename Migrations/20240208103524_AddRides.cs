using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class AddRides : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Finish = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d92c5ef9-6f03-48da-a258-f91da35bfd49", "AQAAAAIAAYagAAAAEEkBUKo1HFQGs9L8sDuRW2k+OPEEu8C3RexYavV2QpHA12iv+2lM8pfUYts2XDWEAw==", "a5e4e67a-40b2-44a1-a324-90883cca5e4e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b279fbad-b818-4da2-a6e0-b464ba0aa035", "AQAAAAIAAYagAAAAEIAHBHJw8d/DNZDoFGqSstMuyii8WU6XCRDbpKT3nbErc9w1l0NathQlTipSbjIIpg==", "496ccfa5-c8a6-42bd-9857-a9cfce508e0f" });

            migrationBuilder.InsertData(
                table: "Points",
                columns: new[] { "UUID", "DriverId", "Point", "RegisterTime", "VehicleId" },
                values: new object[,]
                {
                    { new Guid("0dd12c88-79da-4af1-80c2-c7bef3a6e23f"), 1, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (3 4)"), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("0f2a7d99-b50c-4600-be12-18426e88bbb0"), 1, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (5 6)"), new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("8bf466d2-70f5-4e33-9308-a1a9dd491696"), 1, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (1 2)"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { new Guid("aa996b72-633a-4a31-bbfa-53b54a4a0cf9"), 1, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (7 8)"), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DeleteData(
                table: "Points",
                keyColumn: "UUID",
                keyValue: new Guid("0dd12c88-79da-4af1-80c2-c7bef3a6e23f"));

            migrationBuilder.DeleteData(
                table: "Points",
                keyColumn: "UUID",
                keyValue: new Guid("0f2a7d99-b50c-4600-be12-18426e88bbb0"));

            migrationBuilder.DeleteData(
                table: "Points",
                keyColumn: "UUID",
                keyValue: new Guid("8bf466d2-70f5-4e33-9308-a1a9dd491696"));

            migrationBuilder.DeleteData(
                table: "Points",
                keyColumn: "UUID",
                keyValue: new Guid("aa996b72-633a-4a31-bbfa-53b54a4a0cf9"));

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
    }
}
