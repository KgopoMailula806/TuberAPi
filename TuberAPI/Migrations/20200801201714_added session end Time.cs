using Microsoft.EntityFrameworkCore.Migrations;

namespace TuberAPI.Migrations
{
    public partial class addedsessionendTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "ClientBookings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Periods",
                table: "ClientBookings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EndTime",
                table: "BookingRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Periods",
                table: "BookingRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ClientBookings");

            migrationBuilder.DropColumn(
                name: "Periods",
                table: "ClientBookings");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "BookingRequests");

            migrationBuilder.DropColumn(
                name: "Periods",
                table: "BookingRequests");
        }
    }
}
