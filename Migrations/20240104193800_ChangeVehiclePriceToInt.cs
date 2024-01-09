using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class ChangeVehiclePriceToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ZeroToHundred",
                table: "Vehicles",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "953db325-4651-4baf-852f-3204120b508b", "AQAAAAIAAYagAAAAELaZtHAHMAnYuDwAZ3FSVL0x8Znn0xYBkULiYpKcXjejgQrYB2ksXLYZEKSq7g80Bg==", "37c82e56-d312-4ab8-8969-8abd3f7cb70b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c20a8ab-f7b0-4fde-921f-28014090d1d7", "AQAAAAIAAYagAAAAEG1yZn7FZn8aBmZaoK+68u6HgQ8wfCKlbmfauh8DPPYCR/thfjvm+WYJO1ZAEMfwSg==", "ee250e24-a5a5-42b8-9d46-99efcd0e3741" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "ZeroToHundred" },
                values: new object[] { 3120000, 8.5f });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "ZeroToHundred" },
                values: new object[] { 2055000, 9.8f });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Price", "ZeroToHundred" },
                values: new object[] { 3120000, 8.5f });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Price", "ZeroToHundred" },
                values: new object[] { 3055000, 26f });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Price", "ZeroToHundred" },
                values: new object[] { 130120000, 1.8f });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ZeroToHundred",
                table: "Vehicles",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Vehicles",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Price", "ZeroToHundred" },
                values: new object[] { 3120000.0, 8.5 });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Price", "ZeroToHundred" },
                values: new object[] { 2055000.0, 9.8000000000000007 });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Price", "ZeroToHundred" },
                values: new object[] { 3120000.0, 8.5 });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Price", "ZeroToHundred" },
                values: new object[] { 3055000.0, 26.0 });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Price", "ZeroToHundred" },
                values: new object[] { 130120000.0, 1.8 });
        }
    }
}
