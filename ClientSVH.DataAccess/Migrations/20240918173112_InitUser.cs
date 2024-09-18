using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClientSVH.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocRecordEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocText = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Hidden = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pkg_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stname = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    runwf = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    mkres = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    sendmess = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    OldSt = table.Column<int>(type: "integer", nullable: false),
                    NewSt = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pkg_status", x => x.id);
                    table.ForeignKey(
                        name: "FK_pkg_status_pkg_status_graph_OldSt",
                        column: x => x.OldSt,
                        principalTable: "pkg_status_graph",
                        principalColumn: "oldst",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "packages",
                columns: table => new
                {
                    pid = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modify_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_packages", x => x.pid);
                    table.ForeignKey(
                        name: "FK_packages_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_packages_pkg_status_status",
                        column: x => x.status,
                        principalTable: "pkg_status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    did = table.Column<long>(type: "bigint", nullable: false),
                    number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    docdate = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 5, nullable: false),
                    modecode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    size_doc = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idmd5 = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    idsha256 = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    modify_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pid = table.Column<int>(type: "integer", nullable: false),
                    docid = table.Column<Guid>(type: "uuid", nullable: false),
                    DocRecordId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documents", x => x.did);
                    table.ForeignKey(
                        name: "FK_documents_DocRecordEntity_DocRecordId",
                        column: x => x.DocRecordId,
                        principalTable: "DocRecordEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_documents_packages_did",
                        column: x => x.did,
                        principalTable: "packages",
                        principalColumn: "pid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_documents_DocRecordId",
                table: "documents",
                column: "DocRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_packages_status",
                table: "packages",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_packages_user_id",
                table: "packages",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_pkg_status_OldSt",
                table: "pkg_status",
                column: "OldSt",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "DocRecordEntity");

            migrationBuilder.DropTable(
                name: "packages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "pkg_status");

            migrationBuilder.DropTable(
                name: "pkg_status_graph");
        }
    }
}
