using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleIdToDriversTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Drivers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25f60ddc-099d-4dd9-9247-60b63b87d2b1", "AQAAAAIAAYagAAAAEAiGNykfXOPqKStNbrmFBIZfkF9ZwpSVuFOg1nYlGA6ekZOLd3VQ8YnK43TU0vZPDw==", "280f398a-a2bc-4b4a-adc9-e855cf280da1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "60aca7b9-020b-42e2-873c-b4ae4fc8e1c8", "AQAAAAIAAYagAAAAEAzBgz8x9iMWHNeYldHkl/HrREZwCPTPwFx915lxI2j5IZr6d/6yieqSbXsPkVq7dg==", "5ce7e57f-6f19-49a4-b4b5-aef6371f5aa8" });
        }
    }
}
