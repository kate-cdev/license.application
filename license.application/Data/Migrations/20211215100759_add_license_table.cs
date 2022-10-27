using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace license.application.Data.Migrations
{
    public partial class add_license_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LicenseId",
                table: "Machines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Machines_LicenseId",
                table: "Machines",
                column: "LicenseId",
                unique: true,
                filter: "[LicenseId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_License_LicenseId",
                table: "Machines",
                column: "LicenseId",
                principalTable: "License",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_License_LicenseId",
                table: "Machines");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropIndex(
                name: "IX_Machines_LicenseId",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "Machines");
        }
    }
}
