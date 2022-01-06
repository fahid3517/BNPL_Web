using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNPL_Web.DatabaseModels.Migrations
{
    public partial class tblupdate1212 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackOfficeUserProfile_AspNetUsers_UserIdId",
                table: "BackOfficeUserProfile");

            migrationBuilder.DropIndex(
                name: "IX_BackOfficeUserProfile_UserIdId",
                table: "BackOfficeUserProfile");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "BackOfficeUserProfile");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BackOfficeUserProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BackOfficeUserProfile");

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "BackOfficeUserProfile",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BackOfficeUserProfile_UserIdId",
                table: "BackOfficeUserProfile",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_BackOfficeUserProfile_AspNetUsers_UserIdId",
                table: "BackOfficeUserProfile",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
