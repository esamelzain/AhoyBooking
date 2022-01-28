using Microsoft.EntityFrameworkCore.Migrations;

namespace AhoyBooking.Migrations
{
    public partial class changehoteltype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "Hotels",
                newName: "HotelType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HotelType",
                table: "Hotels",
                newName: "UserType");
        }
    }
}
