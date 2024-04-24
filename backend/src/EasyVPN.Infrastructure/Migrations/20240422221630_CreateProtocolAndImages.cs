using System;
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
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Servers",
                newName: "Version");

            migrationBuilder.AddColumn<Guid>(
                name: "ProtocolId",
                table: "Servers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "ConnectionTickets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Protocols",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocols", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servers_ProtocolId",
                table: "Servers",
                column: "ProtocolId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_Protocols_ProtocolId",
                table: "Servers",
                column: "ProtocolId",
                principalTable: "Protocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_Protocols_ProtocolId",
                table: "Servers");

            migrationBuilder.DropTable(
                name: "Protocols");

            migrationBuilder.DropIndex(
                name: "IX_Servers_ProtocolId",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "ProtocolId",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "ConnectionTickets");

            migrationBuilder.RenameColumn(
                name: "Version",
                table: "Servers",
                newName: "Type");
        }
    }
}
