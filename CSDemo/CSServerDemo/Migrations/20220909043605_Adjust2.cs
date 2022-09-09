using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSServerDemo.Migrations
{
    public partial class Adjust2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "cs_user_account",
                type: "datetime",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.CreateIndex(
                name: "ACCOUNT_UNIQUE",
                table: "cs_user_account",
                column: "account",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ACCOUNT_UNIQUE",
                table: "cs_user_account");

            migrationBuilder.AlterColumn<DateTime>(
                name: "create_at",
                table: "cs_user_account",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");
        }
    }
}
