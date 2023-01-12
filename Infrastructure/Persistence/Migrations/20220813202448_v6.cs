using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_verification_email_codes_users_UserId",
                table: "verification_email_codes");

            migrationBuilder.DropIndex(
                name: "IX_verification_email_codes_UserId",
                table: "verification_email_codes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "verification_email_codes");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_users_email",
                table: "users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_verification_email_codes_email",
                table: "verification_email_codes",
                column: "email");

            migrationBuilder.AddForeignKey(
                name: "FK_verification_email_codes_users_email",
                table: "verification_email_codes",
                column: "email",
                principalTable: "users",
                principalColumn: "email",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_verification_email_codes_users_email",
                table: "verification_email_codes");

            migrationBuilder.DropIndex(
                name: "IX_verification_email_codes_email",
                table: "verification_email_codes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_users_email",
                table: "users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "verification_email_codes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_verification_email_codes_UserId",
                table: "verification_email_codes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_verification_email_codes_users_UserId",
                table: "verification_email_codes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
