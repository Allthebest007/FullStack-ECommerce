using Microsoft.EntityFrameworkCore.Migrations;

namespace InveonECommerce.Data.DAL.Migrations
{
    public partial class orderEntityUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

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
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "8b5cd512-580a-4734-b30c-2d76845a1790");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c189c648-c905-4c83-a1c9-5fbb0ce8db7c",
                column: "ConcurrencyStamp",
                value: "c9e65c81-4125-4672-be9b-259705762f83");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df39363d-11c5-4d9e-b45e-a27cf4ac064f", "AQAAAAEAACcQAAAAECet8/Sxz2CZEtuggMOS3UpW1lxACiNokgKTHJW/x3vgO7lyGKWE2DZoOoYznyjyMA==", "4a5c6c3a-ad2b-4c08-ab1f-1e89dd3677e4" });
        }
    }
}
