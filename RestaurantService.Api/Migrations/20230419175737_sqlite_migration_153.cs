using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantService.Api.Migrations
{
    /// <inheritdoc />
    public partial class sqlite_migration_153 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuItemId",
                table: "MenuItemOrdered",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "MenuItemOrdered");
        }
    }
}
