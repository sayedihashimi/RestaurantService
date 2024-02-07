using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyRestaurantApi.Migrations
{
    /// <inheritdoc />
    public partial class sqlite_migration_497 : Migration
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
                    MenuItemId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    { 1001, 0, "Two eggs sunny side up and wheat toast", "Two eggs and toast", 6.09m },
                    { 1002, 0, "Small steak and two eggs", "Steak and Two eggs", 10.09m },
                    { 2000, 1, "Enjoy a half portion of crispy fried chicken paired with a fluffy Belgian waffle, dusted with powdered sugar and accompanied by luscious strawberry butter. Complemented with our special sweet and spicy sauce, it's a true culinary delight.", "Fried Chicken & Waffle", 16.79m },
                    { 2001, 1, "Savor our seasoned sirloin steak alongside fluffy scrambled eggs and roasted grape tomatoes, all atop golden hashbrown potatoes and finished with a decadent drizzle of hollandaise sauce.", "Steak & Scramble Bowl", 13.79m },
                    { 2002, 1, "Delight in our distinctive Belgian waffle, adorned with a luscious drizzle of strawberry cream cheese icing, fresh strawberries, delectable cobbler crumble, and a sprinkle of powdered sugar.", "Strawberry Shortcake Waffle", 10.49m },
                    { 2003, 1, "Indulge in our tender country fried steak, smothered in rich homemade sausage gravy, accompanied by two eggs cooked to your preference and your choice of side.", "Country Fried Steak", 16.99m },
                    { 2004, 1, "Savor our tender, lightly fried chicken, enhanced with a zesty pineapple-orange sauce and garnished with fresh scallions. Presented on a bed of seasoned white rice alongside a colorful medley of shredded carrots, red cabbage, corn, green peppers, and onion.", "Tangy Chicken Bowl", 10.99m },
                    { 2005, 1, "It's a cheese burger without the cheese", "Hamburger", 3.68m },
                    { 2006, 1, "It's a cheese burger without the cheese, with two beef patties", "Hamburger - double", 5.7m },
                    { 2007, 1, "A hamburger with cheese", "Cheeseburger", 4.09m },
                    { 2008, 1, "A hamburger with cheese, with two beef patties", "Cheeseburger - double", 5.09m },
                    { 2009, 1, "Mushroom & Swiss burger", "Mushroom & Swiss burger", 4.59m },
                    { 2010, 1, "Mushroom & Swiss burger, with two beef patties", "Mushroom & Swiss burger - double", 6.09m },
                    { 3000, 2, "Ribeye Steak and mashed potatoes", "Steak and mashed potatoes", 15.09m },
                    { 3001, 2, "Golden beer-battered white fish, fried to perfection, accompanied by tartar sauce, coleslaw, and seasoned fries.", "Fish and chips", 14.49m },
                    { 3002, 2, "Sam's renowned creation: succulent griddle-seared meatloaf smothered in a luscious brown gravy, served alongside velvety mashed potatoes and tender steamed green beans.", "Sam's famous meatloaf", 14.49m },
                    { 3003, 2, "Indulge in Sam's signature dish: tender chicken, carrots, celery, and corn bathed in our velvety cream sauce, all crowned with our unique homemade biscuit crust.", "Sam's famous Chicken Pot Pie", 14.49m }
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
