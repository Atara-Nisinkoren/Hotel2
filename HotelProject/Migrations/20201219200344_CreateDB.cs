using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelProject.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomsOrders",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "RoomsOrders",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientID",
                table: "Order",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomsOrders",
                table: "RoomsOrders",
                columns: new[] { "RoomId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomsOrders_OrderId",
                table: "RoomsOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_TypeId",
                table: "Room",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientID",
                table: "Order",
                column: "ClientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomsOrders",
                table: "RoomsOrders");

            migrationBuilder.DropIndex(
                name: "IX_RoomsOrders_OrderId",
                table: "RoomsOrders");

            migrationBuilder.DropIndex(
                name: "IX_Room_TypeId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Order_ClientID",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "RoomsOrders",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "RoomsOrders",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ClientID",
                table: "Order",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
