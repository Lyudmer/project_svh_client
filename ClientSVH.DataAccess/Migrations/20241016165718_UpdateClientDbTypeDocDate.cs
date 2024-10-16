using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientSVH.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientDbTypeDocDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "docdate",
                table: "documents",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "docdate",
                table: "documents",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
