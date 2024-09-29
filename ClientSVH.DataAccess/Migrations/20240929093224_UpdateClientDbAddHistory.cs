using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientSVH.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientDbAddHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "history_pkg",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Pid = table.Column<int>(type: "integer", nullable: false),
                    oldst = table.Column<int>(type: "integer", nullable: false),
                    newst = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: true),
                    comment = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history_pkg", x => x.id);
                    table.ForeignKey(
                        name: "FK_history_pkg_pkg_status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "pkg_status",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_history_pkg_StatusId",
                table: "history_pkg",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "history_pkg");
        }
    }
}
