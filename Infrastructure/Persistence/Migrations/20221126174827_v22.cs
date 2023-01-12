using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class v22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "timestamp",
                table: "endpoints_logs",
                newName: "responded_at");

            migrationBuilder.AlterColumn<DateTime>(
                name: "responded_at",
                table: "endpoints_logs",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "responded_at",
                table: "endpoints_logs",
                newName: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "timestamp",
                table: "endpoints_logs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }
    }
}
