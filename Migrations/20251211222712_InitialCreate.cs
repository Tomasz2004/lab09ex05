using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace lab09webAPp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 11, 22, 27, 11, 638, DateTimeKind.Utc).AddTicks(8316), "High-performance laptop", "Laptop", 3999.99m, 15 },
                    { 2, new DateTime(2025, 12, 11, 22, 27, 11, 638, DateTimeKind.Utc).AddTicks(8318), "Wireless gaming mouse", "Mouse", 199.99m, 50 },
                    { 3, new DateTime(2025, 12, 11, 22, 27, 11, 638, DateTimeKind.Utc).AddTicks(8320), "Mechanical keyboard", "Keyboard", 399.99m, 30 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
