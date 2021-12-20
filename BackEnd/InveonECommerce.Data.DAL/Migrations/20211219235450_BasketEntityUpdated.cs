using Microsoft.EntityFrameworkCore.Migrations;

namespace InveonECommerce.Data.DAL.Migrations
{
    public partial class BasketEntityUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_BasketId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "bf17ed44-c42f-44e6-9223-bca06714e373");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c189c648-c905-4c83-a1c9-5fbb0ce8db7c",
                column: "ConcurrencyStamp",
                value: "19fdb0a2-2086-466e-b431-1dc44f261a0c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "55e063d1-c344-47ca-be17-02d7c869c9af", "AQAAAAEAACcQAAAAEIX4h2iWEDjr/RyO51y/ppmK8gSbShYqCI2W90lX977lkpqsJMluuQNPQIC8LX7rfg==", "1c069d47-68d9-42cd-b994-430016a6b92c" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BasketId",
                table: "Orders",
                column: "BasketId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_BasketId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "4158e43e-5568-4736-a0b6-95a8daa752d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c189c648-c905-4c83-a1c9-5fbb0ce8db7c",
                column: "ConcurrencyStamp",
                value: "600a7a28-68f7-425a-a55f-3a89d9d72c8c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9935f88f-123d-49c0-8203-cec8cd103bca", "AQAAAAEAACcQAAAAEPu/grFlCHCVE1AoriId/wbhRiGAiQiN6/a3O2GoRz+tg37d/v9C7KnblCIBZXM+fA==", "751213ee-9133-4bd6-9107-2e9dd4f33ccd" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BasketId",
                table: "Orders",
                column: "BasketId");
        }
    }
}
