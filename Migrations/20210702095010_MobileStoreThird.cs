using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileServices.Migrations
{
    public partial class MobileStoreThird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SellingPrice",
                table: "Sales",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "Sales");
        }
    }
}
