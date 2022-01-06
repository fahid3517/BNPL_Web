using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNPL_Web.DatabaseModels.Migrations
{
    public partial class tblupdate12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProfile_AspNetUsers_UserIdId",
                table: "CustomerProfile");

            migrationBuilder.DropIndex(
                name: "IX_CustomerProfile_UserIdId",
                table: "CustomerProfile");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "CustomerProfile");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CustomerProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomerProfile");

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "CustomerProfile",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfile_UserIdId",
                table: "CustomerProfile",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProfile_AspNetUsers_UserIdId",
                table: "CustomerProfile",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
