using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientSVH.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientDbAddTypeDoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "doctype",
                table: "documents",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "doctype",
                table: "documents");
        }
    }
}
