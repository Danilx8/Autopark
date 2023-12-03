using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverAndEnterpriseModelsAndConnectionsBetweenExistingOnes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnterpriseId",
                table: "Vehicles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Enterprises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    EnterpriseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Enterprises_EnterpriseId",
                        column: x => x.EnterpriseId,
                        principalTable: "Enterprises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "Enterprises",
                columns: new[] { "Id", "City", "Name" },
                values: new object[,]
                {
                    { 1, "Москва", "Мосгортранс" },
                    { 2, "Мюнхен", "DHL" },
                    { 3, "Новосибирск", "Сибтранс" }
                });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DriverId", "EnterpriseId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DriverId", "EnterpriseId" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DriverId", "EnterpriseId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DriverId", "EnterpriseId" },
                values: new object[] { null, 2 });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DriverId", "EnterpriseId" },
                values: new object[] { 3, 3 });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "EnterpriseId", "Name", "Salary" },
                values: new object[,]
                {
                    { 1, 1, "Василий", 45000 },
                    { 2, 2, "Геннадий", 30000 },
                    { 3, 3, "Евгений", 50000 },
                    { 4, 1, "Александр", 55000 },
                    { 5, 2, "Григорий", 35000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_DriverId",
                table: "Vehicles",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_EnterpriseId",
                table: "Vehicles",
                column: "EnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_EnterpriseId",
                table: "Drivers",
                column: "EnterpriseId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverVehicle_AssignedDriversId",
                table: "DriverVehicle",
                column: "AssignedDriversId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Drivers_DriverId",
                table: "Vehicles",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Enterprises_EnterpriseId",
                table: "Vehicles",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Drivers_DriverId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Enterprises_EnterpriseId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "DriverVehicle");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Enterprises");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_DriverId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_EnterpriseId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
