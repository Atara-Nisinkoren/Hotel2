using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelProject.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "RoomsOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "RoomsOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomsOrders_OrderId1",
                table: "RoomsOrders",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsOrders_RoomId1",
                table: "RoomsOrders",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsOrders_Order_OrderId1",
                table: "RoomsOrders",
                column: "OrderId1",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomsOrders_Room_RoomId1",
                table: "RoomsOrders",
                column: "RoomId1",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomsOrders_Order_OrderId1",
                table: "RoomsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomsOrders_Room_RoomId1",
                table: "RoomsOrders");

            migrationBuilder.DropIndex(
                name: "IX_RoomsOrders_OrderId1",
                table: "RoomsOrders");

            migrationBuilder.DropIndex(
                name: "IX_RoomsOrders_RoomId1",
                table: "RoomsOrders");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "RoomsOrders");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "RoomsOrders");
        }
    }
}
