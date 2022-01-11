using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNPL_Web.DatabaseModels.Migrations
{
    public partial class TblAdd12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTPVerification_AspNetUsers_AspNetUserId",
                table: "OTPVerification");

            migrationBuilder.DropIndex(
                name: "IX_OTPVerification_AspNetUserId",
                table: "OTPVerification");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "OTPVerification");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OTPVerification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OTPVerification");

            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "OTPVerification",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OTPVerification_AspNetUserId",
                table: "OTPVerification",
                column: "AspNetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OTPVerification_AspNetUsers_AspNetUserId",
                table: "OTPVerification",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
