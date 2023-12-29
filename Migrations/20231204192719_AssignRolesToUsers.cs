using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class AssignRolesToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "1", "2" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "576a6db8-070e-4328-8279-14087aea19fe", "AQAAAAIAAYagAAAAEBmBMpQUuLXLqiE7/UXx8JvoYccelJbThMUv+okMn5EFCZTTUSjBfupiX0p6hAA/jQ==", "1ac01839-9814-4517-8cc9-e2a7565c1862" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0cf2548c-3798-4e05-aebc-0a884898ade7", "AQAAAAIAAYagAAAAEG0GHChyh7H1GGmfq4GlYRtTM0CCyrnByzr+DKof0i9vn+q5wuln5eTyFW+/e+9FdA==", "2b4917e5-290f-473f-b9de-691ab1707bd0" });
        }
    }
}
