using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace CSServerDemo.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cs_user_account",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    account = table.Column<string>(type: "text", nullable: false),
                    nickname = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    create_by = table.Column<long>(type: "bigint", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_by = table.Column<long>(type: "bigint", nullable: true),
                    last_login_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cs_user_account", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cs_user_account");
        }
    }
}
