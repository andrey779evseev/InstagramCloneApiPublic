using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class v20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "endpoints_logs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    access_token = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    path = table.Column<string>(type: "text", nullable: false),
                    request = table.Column<object>(type: "jsonb", nullable: false),
                    response = table.Column<object>(type: "jsonb", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status_code = table.Column<int>(type: "integer", nullable: false),
                    error_description = table.Column<string>(type: "text", nullable: true),
                    exception_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_endpoints_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_endpoints_logs_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_endpoints_logs_user_id",
                table: "endpoints_logs",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "endpoints_logs");
        }
    }
}
