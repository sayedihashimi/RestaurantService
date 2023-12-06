using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRestaurantApi.Migrations
{
    /// <inheritdoc />
    public partial class sqlite_migration_891 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemOrdered_TogoOrderId",
                table: "MenuItemOrdered",
                column: "TogoOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TogoOrder_CustomerId",
                table: "TogoOrder",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOrdered_TogoOrder_TogoOrderId",
                table: "MenuItemOrdered",
                column: "TogoOrderId",
                principalTable: "TogoOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOrdered_TogoOrder_TogoOrderId",
                table: "MenuItemOrdered");

            migrationBuilder.DropTable(
                name: "TogoOrder");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemOrdered_TogoOrderId",
                table: "MenuItemOrdered");
        }
    }
}
