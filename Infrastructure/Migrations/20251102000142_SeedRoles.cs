using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Users_OwnerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntityServiceEntity_Orders_OrdersId",
                table: "OrderEntityServiceEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderEntityServiceEntity_Services_SelectedServicesId",
                table: "OrderEntityServiceEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderEntityServiceEntity",
                table: "OrderEntityServiceEntity");

            migrationBuilder.RenameTable(
                name: "OrderEntityServiceEntity",
                newName: "OrderServices");

            migrationBuilder.RenameIndex(
                name: "IX_OrderEntityServiceEntity_SelectedServicesId",
                table: "OrderServices",
                newName: "IX_OrderServices_SelectedServicesId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderServices",
                table: "OrderServices",
                columns: new[] { "OrdersId", "SelectedServicesId" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 3, "User" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Users_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderServices_Orders_OrdersId",
                table: "OrderServices",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderServices_Services_SelectedServicesId",
                table: "OrderServices",
                column: "SelectedServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Users_OwnerId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderServices_Orders_OrdersId",
                table: "OrderServices");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderServices_Services_SelectedServicesId",
                table: "OrderServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderServices",
                table: "OrderServices");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "OrderServices",
                newName: "OrderEntityServiceEntity");

            migrationBuilder.RenameIndex(
                name: "IX_OrderServices_SelectedServicesId",
                table: "OrderEntityServiceEntity",
                newName: "IX_OrderEntityServiceEntity_SelectedServicesId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Orders",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderEntityServiceEntity",
                table: "OrderEntityServiceEntity",
                columns: new[] { "OrdersId", "SelectedServicesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Users_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntityServiceEntity_Orders_OrdersId",
                table: "OrderEntityServiceEntity",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderEntityServiceEntity_Services_SelectedServicesId",
                table: "OrderEntityServiceEntity",
                column: "SelectedServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
