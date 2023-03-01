using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class v18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<Guid>>(
                name: "following",
                table: "users",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<Guid>),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "followers",
                table: "users",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(List<Guid>),
                oldType: "jsonb",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<Guid>>(
                name: "following",
                table: "users",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<Guid>),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<List<Guid>>(
                name: "followers",
                table: "users",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(List<Guid>),
                oldType: "jsonb");
        }
    }
}
