using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantService.Api.Migrations
{
    /// <inheritdoc />
    public partial class sqlite_migration_347 : Migration
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
