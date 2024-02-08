using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class AddAcquireTimeColumnForVehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcquireTime",
                table: "Vehicles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AcquireTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                column: "AcquireTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "AcquireTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                column: "AcquireTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                column: "AcquireTime",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcquireTime",
                table: "Vehicles");

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
        }
    }
}
