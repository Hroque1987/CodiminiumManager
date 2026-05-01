using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CondominiumManager.Condominium.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMembership_Condominium : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Memberships",
                schema: "Condominium",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => new { x.UserId, x.BuildingId });
                    table.ForeignKey(
                        name: "FK_Memberships_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "Condominium",
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_BuildingId",
                schema: "Condominium",
                table: "Memberships",
                column: "BuildingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Memberships",
                schema: "Condominium");
        }
    }
}
