using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNPL_Web.DatabaseModels.Migrations
{
    public partial class tblupdate100 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProfile_AspNetUsers_ApplicationUserId",
                table: "CustomerProfile");

            migrationBuilder.DropIndex(
                name: "IX_CustomerProfile_ApplicationUserId",
                table: "CustomerProfile");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CustomerProfile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CustomerProfile",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfile_ApplicationUserId",
                table: "CustomerProfile",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProfile_AspNetUsers_ApplicationUserId",
                table: "CustomerProfile",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
