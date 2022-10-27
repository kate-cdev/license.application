using Microsoft.EntityFrameworkCore.Migrations;

namespace license.application.Data.Migrations
{
    public partial class change_column_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UniqueIs",
                table: "Machines",
                newName: "UniqueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UniqueId",
                table: "Machines",
                newName: "UniqueIs");
        }
    }
}
