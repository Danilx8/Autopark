using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteForDriverVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Drivers_DriverId",
                table: "Vehicles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f8a5789-6e29-40bd-8aae-2461859061e4", "AQAAAAIAAYagAAAAEPcE8q4XsYLkE/7I8ufnqbAHxYTFDGTfM5vR9Wy12qW+/L+K3Xj72mLd5GnCnS3Hyw==", "8bafea7d-a3ef-4d10-90c5-7fc690bb0c0e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d88c3cb4-29a8-416f-9395-3aca075bef33", "AQAAAAIAAYagAAAAEFuZNOqyzYhwln1ccceXKkGV8uFUR+B3hJxSPGUXj4W3OaTEUwzCCvV8HPEoTQhHfw==", "06343336-993a-4c14-9a2a-8528d558dd6f" });

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Drivers_DriverId",
                table: "Vehicles",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Drivers_DriverId",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7870cba3-35ad-4142-855f-2d0235d9094c", "AQAAAAIAAYagAAAAENOxird6zO0was5hkNhFgldd/mD/vZXEjfIEPLYwqWsQsbZudLyxmViCtzdxt3rAiw==", "a740c859-a427-42cc-9824-7f7b1966bac4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7286e32a-902f-4079-b9d0-01ae6b17b738", "AQAAAAIAAYagAAAAEJrQm18wjvjQGi/57dCGtSDCZLmRBtUU1eBiWK8Datykx/1TYNo+zY7K2A22bCn6AQ==", "2fe09bb9-1b7b-426d-a1ac-9cfeb9be21a9" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1,
                column: "VehicleId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 2,
                column: "VehicleId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 3,
                column: "VehicleId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 4,
                column: "VehicleId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 5,
                column: "VehicleId",
                value: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Drivers_DriverId",
                table: "Vehicles",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }
    }
}
