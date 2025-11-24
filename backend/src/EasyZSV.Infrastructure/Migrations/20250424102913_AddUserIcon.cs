using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyZsV.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Users");
        }
    }
}
