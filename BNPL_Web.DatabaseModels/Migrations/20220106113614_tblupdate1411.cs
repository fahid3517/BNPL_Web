using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNPL_Web.DatabaseModels.Migrations
{
    public partial class tblupdate1411 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SystemUsersProfile");

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "SystemUsersProfile",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CustomerProfile",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsersProfile_UserIdId",
                table: "SystemUsersProfile",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfile_ApplicationUserId",
                table: "CustomerProfile",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRole_RoleId",
                table: "ApplicationUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRole_UserId",
                table: "ApplicationUserRole",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProfile_AspNetUsers_ApplicationUserId",
                table: "CustomerProfile",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUsersProfile_AspNetUsers_UserIdId",
                table: "SystemUsersProfile",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProfile_AspNetUsers_ApplicationUserId",
                table: "CustomerProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemUsersProfile_AspNetUsers_UserIdId",
                table: "SystemUsersProfile");

            migrationBuilder.DropTable(
                name: "ApplicationUserRole");

            migrationBuilder.DropIndex(
                name: "IX_SystemUsersProfile_UserIdId",
                table: "SystemUsersProfile");

            migrationBuilder.DropIndex(
                name: "IX_CustomerProfile_ApplicationUserId",
                table: "CustomerProfile");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "SystemUsersProfile");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CustomerProfile");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SystemUsersProfile",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
