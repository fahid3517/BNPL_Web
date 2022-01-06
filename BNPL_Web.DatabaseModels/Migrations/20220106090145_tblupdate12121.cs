using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNPL_Web.DatabaseModels.Migrations
{
    public partial class tblupdate12121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemUsersProfile_AspNetUsers_UserIdId",
                table: "SystemUsersProfile");

            migrationBuilder.DropIndex(
                name: "IX_SystemUsersProfile_UserIdId",
                table: "SystemUsersProfile");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "SystemUsersProfile");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "SystemUsersProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SystemUsersProfile",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SystemUsersProfile");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "SystemUsersProfile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "SystemUsersProfile",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsersProfile_UserIdId",
                table: "SystemUsersProfile",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUsersProfile_AspNetUsers_UserIdId",
                table: "SystemUsersProfile",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
