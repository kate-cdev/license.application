using Microsoft.EntityFrameworkCore.Migrations;

namespace license.application.Data.Migrations
{
    public partial class adding_phone_email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Machines");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Machines");
        }
    }
}
