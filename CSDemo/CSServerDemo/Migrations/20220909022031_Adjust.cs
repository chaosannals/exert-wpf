using Microsoft.EntityFrameworkCore.Migrations;

namespace CSServerDemo.Migrations
{
    public partial class Adjust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "cs_user_account",
                type: "CHAR(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nickname",
                table: "cs_user_account",
                type: "VARCHAR(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "account",
                table: "cs_user_account",
                type: "VARCHAR(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "password",
                table: "cs_user_account",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "CHAR(64)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nickname",
                table: "cs_user_account",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "account",
                table: "cs_user_account",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)");
        }
    }
}
