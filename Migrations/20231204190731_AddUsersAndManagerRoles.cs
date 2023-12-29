using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersAndManagerRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EnterpriseManager",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ManagedEnterpriseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnterpriseManager", x => new { x.UserId, x.ManagedEnterpriseId });
                    table.ForeignKey(
                        name: "FK_EnterpriseManager_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnterpriseManager_Enterprises_ManagedEnterpriseId",
                        column: x => x.ManagedEnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "1", null, "Manager", "manager", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "576a6db8-070e-4328-8279-14087aea19fe", null, false, false, null, null, "SAM", "AQAAAAIAAYagAAAAEBmBMpQUuLXLqiE7/UXx8JvoYccelJbThMUv+okMn5EFCZTTUSjBfupiX0p6hAA/jQ==", null, false, "1ac01839-9814-4517-8cc9-e2a7565c1862", false, "Manager Sam" },
                    { "2", 0, "0cf2548c-3798-4e05-aebc-0a884898ade7", null, false, false, null, null, "TOM", "AQAAAAIAAYagAAAAEG0GHChyh7H1GGmfq4GlYRtTM0CCyrnByzr+DKof0i9vn+q5wuln5eTyFW+/e+9FdA==", null, false, "2b4917e5-290f-473f-b9de-691ab1707bd0", false, "Manager Tom" }
                });

            migrationBuilder.InsertData(
                table: "EnterpriseManager",
                columns: new[] { "ManagedEnterpriseId", "UserId" },
                values: new object[,]
                {
                    { 1, "1" },
                    { 2, "1" },
                    { 2, "2" },
                    { 3, "2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnterpriseManager_ManagedEnterpriseId",
                table: "EnterpriseManager",
                column: "ManagedEnterpriseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnterpriseManager");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}
