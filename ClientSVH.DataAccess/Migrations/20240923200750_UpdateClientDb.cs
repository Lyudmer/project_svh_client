using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClientSVH.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_DocRecordEntity_DocRecordId",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_documents_packages_did",
                table: "documents");

            migrationBuilder.DropForeignKey(
                name: "FK_pkg_status_pkg_status_graph_OldSt",
                table: "pkg_status");

            migrationBuilder.DropTable(
                name: "DocRecordEntity");

            migrationBuilder.DropTable(
                name: "pkg_status_graph");

            migrationBuilder.DropIndex(
                name: "IX_pkg_status_OldSt",
                table: "pkg_status");

            migrationBuilder.DropIndex(
                name: "IX_documents_DocRecordId",
                table: "documents");

            migrationBuilder.DropColumn(
                name: "NewSt",
                table: "pkg_status");

            migrationBuilder.DropColumn(
                name: "OldSt",
                table: "pkg_status");

            migrationBuilder.DropColumn(
                name: "DocRecordId",
                table: "documents");

            migrationBuilder.AlterColumn<long>(
                name: "pid",
                table: "documents",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "did",
                table: "documents",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_documents_pid",
                table: "documents",
                column: "pid");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_packages_pid",
                table: "documents",
                column: "pid",
                principalTable: "packages",
                principalColumn: "pid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_documents_packages_pid",
                table: "documents");

            migrationBuilder.DropIndex(
                name: "IX_documents_pid",
                table: "documents");

            migrationBuilder.AddColumn<int>(
                name: "NewSt",
                table: "pkg_status",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OldSt",
                table: "pkg_status",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "pid",
                table: "documents",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "did",
                table: "documents",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "DocRecordId",
                table: "documents",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DocRecordEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DocId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocText = table.Column<string>(type: "text", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocRecordEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pkg_status_graph",
                columns: table => new
                {
                    oldst = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    newst = table.Column<int>(type: "integer", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pkg_status_graph", x => x.oldst);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pkg_status_OldSt",
                table: "pkg_status",
                column: "OldSt",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_documents_DocRecordId",
                table: "documents",
                column: "DocRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_DocRecordEntity_DocRecordId",
                table: "documents",
                column: "DocRecordId",
                principalTable: "DocRecordEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_documents_packages_did",
                table: "documents",
                column: "did",
                principalTable: "packages",
                principalColumn: "pid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pkg_status_pkg_status_graph_OldSt",
                table: "pkg_status",
                column: "OldSt",
                principalTable: "pkg_status_graph",
                principalColumn: "oldst",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
