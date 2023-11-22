using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Autopark.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationCreateVehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ZeroToHundred = table.Column<double>(type: "float", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    HorsePower = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "HorsePower", "Mileage", "Name", "Price", "Year", "ZeroToHundred" },
                values: new object[,]
                {
                    { 1, 5000, 2000, "Chery Tiggo Pro Max 8", 3120000.0, 2023, 8.5 },
                    { 2, 5500, 4000, "Chery Tiggo Pro Max 7", 2055000.0, 2022, 9.8000000000000007 },
                    { 3, 5000, 10000, "Chery Tiggo Pro Max 6", 3120000.0, 2021, 8.5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
