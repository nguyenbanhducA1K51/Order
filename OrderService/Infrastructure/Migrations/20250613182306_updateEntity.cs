using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_Addresses_AddressId",
                table: "User_Address");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_Customers_CustomerId",
                table: "User_Address");

            migrationBuilder.DropIndex(
                name: "IX_User_Address_AddressId",
                table: "User_Address");

            migrationBuilder.DropIndex(
                name: "IX_User_Address_CustomerId",
                table: "User_Address");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "User_Address");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "User_Address");

            migrationBuilder.CreateIndex(
                name: "IX_User_Address_Address_Id",
                table: "User_Address",
                column: "Address_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Address_Customer_Id",
                table: "User_Address",
                column: "Customer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_Addresses_Address_Id",
                table: "User_Address",
                column: "Address_Id",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_Customers_Customer_Id",
                table: "User_Address",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_Addresses_Address_Id",
                table: "User_Address");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Address_Customers_Customer_Id",
                table: "User_Address");

            migrationBuilder.DropIndex(
                name: "IX_User_Address_Address_Id",
                table: "User_Address");

            migrationBuilder.DropIndex(
                name: "IX_User_Address_Customer_Id",
                table: "User_Address");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "User_Address",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "User_Address",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_Address_AddressId",
                table: "User_Address",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Address_CustomerId",
                table: "User_Address",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_Addresses_AddressId",
                table: "User_Address",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Address_Customers_CustomerId",
                table: "User_Address",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
