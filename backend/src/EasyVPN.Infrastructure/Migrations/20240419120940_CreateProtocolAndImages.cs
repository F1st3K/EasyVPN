using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyVPN.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateProtocolAndImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Protocols_ProtocolId",
                table: "Servers");

            migrationBuilder.DropIndex(
                name: "IX_Servers_ProtocolId",
                table: "Servers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Servers_ProtocolId",
                table: "Servers",
                column: "ProtocolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Protocols_ProtocolId",
                table: "Servers",
                column: "ProtocolId",
                principalTable: "Protocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
