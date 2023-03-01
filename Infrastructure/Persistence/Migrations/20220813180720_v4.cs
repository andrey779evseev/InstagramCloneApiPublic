using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "verification_email_codes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false),
                    used = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_verification_email_codes", x => x.id);
                    table.UniqueConstraint("AK_verification_email_codes_email", x => x.email);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_verification_email_codes_email",
                table: "users",
                column: "email",
                principalTable: "verification_email_codes",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_verification_email_codes_email",
                table: "users");

            migrationBuilder.DropTable(
                name: "verification_email_codes");

            migrationBuilder.DropIndex(
                name: "IX_users_email",
                table: "users");
        }
    }
}
