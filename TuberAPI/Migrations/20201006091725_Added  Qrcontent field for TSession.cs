using Microsoft.EntityFrameworkCore.Migrations;

namespace TuberAPI.Migrations
{
    public partial class AddedQrcontentfieldforTSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QRContent",
                table: "Tutorial_Sessions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRContent",
                table: "Tutorial_Sessions");
        }
    }
}
