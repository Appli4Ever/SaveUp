using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaveUp.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class loginBlock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LoginBlocked",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LoginTries",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginBlocked",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LoginTries",
                table: "Users");
        }
    }
}
