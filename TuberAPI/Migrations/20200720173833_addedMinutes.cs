using Microsoft.EntityFrameworkCore.Migrations;

namespace TuberAPI.Migrations
{
    public partial class addedMinutes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Minutes",
                table: "Meetings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Minutes",
                table: "Meetings");
        }
    }
}
