using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CondominiumManager.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPassword_Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                schema: "Identity",
                table: "Users");
        }
    }
}
