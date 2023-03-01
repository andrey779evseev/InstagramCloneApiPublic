using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class v21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "access_token",
                table: "endpoints_logs",
                type: "text",
                nullable: true,
                defaultValue: "Anonymous",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "access_token",
                table: "endpoints_logs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldDefaultValue: "Anonymous");
        }
    }
}
