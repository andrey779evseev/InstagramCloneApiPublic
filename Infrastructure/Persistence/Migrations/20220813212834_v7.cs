using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "verification_email_codes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_users_email",
                table: "users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_users_email",
                table: "users",
                column: "email");

            migrationBuilder.CreateTable(
                name: "verification_email_codes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    used = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_verification_email_codes", x => x.id);
                    table.ForeignKey(
                        name: "FK_verification_email_codes_users_email",
                        column: x => x.email,
                        principalTable: "users",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_verification_email_codes_email",
                table: "verification_email_codes",
                column: "email");
        }
    }
}
