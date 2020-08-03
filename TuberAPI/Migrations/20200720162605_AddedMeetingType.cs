using Microsoft.EntityFrameworkCore.Migrations;

namespace TuberAPI.Migrations
{
    public partial class AddedMeetingType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Meetings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Meetings");
        }
    }
}
