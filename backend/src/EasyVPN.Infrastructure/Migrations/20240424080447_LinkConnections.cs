using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyVPN.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LinkConnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Servers_ProtocolId",
                table: "Servers");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_ProtocolId",
                table: "Servers",
                column: "ProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_ClientId",
                table: "Connections",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_ServerId",
                table: "Connections",
                column: "ServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Servers_ServerId",
                table: "Connections",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Users_ClientId",
                table: "Connections",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Servers_ServerId",
                table: "Connections");

            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Users_ClientId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Servers_ProtocolId",
                table: "Servers");

            migrationBuilder.DropIndex(
                name: "IX_Connections_ClientId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_ServerId",
                table: "Connections");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_ProtocolId",
                table: "Servers",
                column: "ProtocolId",
                unique: true);
        }
    }
}
