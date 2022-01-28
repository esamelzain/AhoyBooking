using Microsoft.EntityFrameworkCore.Migrations;

namespace AhoyBooking.Migrations
{
    public partial class urlToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "HotelImages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "HotelImages");
        }
    }
}
