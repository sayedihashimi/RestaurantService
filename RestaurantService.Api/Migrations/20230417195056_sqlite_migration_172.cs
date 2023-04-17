using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantService.Api.Migrations
{
    /// <inheritdoc />
    public partial class sqlite_migration_172 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TogoOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderCreated = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Subtotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    Tax = table.Column<decimal>(type: "TEXT", nullable: true),
                    Total = table.Column<decimal>(type: "TEXT", nullable: true),
                    PaymentMethod = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TogoOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TogoOrder_Contact_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Contact",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MenuItemOrdered",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TogoOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemOrdered", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItemOrdered_TogoOrder_TogoOrderId",
                        column: x => x.TogoOrderId,
                        principalTable: "TogoOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "sayed@example.com", "Sayed Hashimi", "555-111-2222" },
                    { 2, "mads@example.com", "Mads Kristensen", "555-111-3333" },
                    { 3, "elineb@example.com", "Eline Barstad", "555-111-4444" },
                    { 4, "theol@example.com", "Theodore Lamy", "555-111-5555" },
                    { 5, "mariaz@example.com", "María Zelaya", "555-111-6666" },
                    { 6, "kubans@example.com", "Kubanychbek Sagynbek", "555-111-7777" },
                    { 7, "deniseb@example.com", "Denise Bourgeois", "555-111-8888" },
                    { 8, "robind@example.com", "Robin Danielsen", "555-111-9999" }
                });

            migrationBuilder.InsertData(
                table: "MenuItem",
                columns: new[] { "Id", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "It's a cheese burger without the cheese", "Hamburger", 3.68m },
                    { 2, 1, "It's a cheese burger without the cheese, with two beef patties", "Hamburger - double", 5.7m },
                    { 3, 1, "A hamburger with cheese", "Cheeseburger", 4.09m },
                    { 4, 1, "A hamburger with cheese, with two beef patties", "Cheeseburger - double", 5.09m },
                    { 5, 1, "Mushroom & Swiss burger", "Mushroom & Swiss burger", 4.59m },
                    { 6, 1, "Mushroom & Swiss burger, with two beef patties", "Mushroom & Swiss burger - double", 6.09m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOrdered_TogoOrderId",
                table: "MenuItemOrdered",
                column: "TogoOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TogoOrder_CustomerId",
                table: "TogoOrder",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropTable(
                name: "MenuItemOrdered");

            migrationBuilder.DropTable(
                name: "TogoOrder");

            migrationBuilder.DropTable(
                name: "Contact");
        }
    }
}
