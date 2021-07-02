using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileServices.Migrations
{
    public partial class MobileStoreSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Sales",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ItemId",
                table: "Sales",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Items_ItemId",
                table: "Sales",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Items_ItemId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ItemId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Sales");
        }
    }
}
