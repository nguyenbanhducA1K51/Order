using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderCustomerRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Customer_Id",
                table: "Orders",
                column: "Customer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_Customer_Id",
                table: "Orders",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_Customer_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Customer_Id",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Customer_Id",
                table: "Orders",
                newName: "CustomerId");
        }
    }
}
