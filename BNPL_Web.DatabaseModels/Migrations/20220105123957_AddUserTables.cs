using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNPL_Web.DatabaseModels.Migrations
{
    public partial class AddUserTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BackOfficeUserProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserIdId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackOfficeUserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BackOfficeUserProfile_AspNetUsers_UserIdId",
                        column: x => x.UserIdId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserIdId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerProfile_AspNetUsers_UserIdId",
                        column: x => x.UserIdId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SystemUsersProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserIdId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUsersProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUsersProfile_AspNetUsers_UserIdId",
                        column: x => x.UserIdId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackOfficeUserProfile_UserIdId",
                table: "BackOfficeUserProfile",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfile_UserIdId",
                table: "CustomerProfile",
                column: "UserIdId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsersProfile_UserIdId",
                table: "SystemUsersProfile",
                column: "UserIdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackOfficeUserProfile");

            migrationBuilder.DropTable(
                name: "CustomerProfile");

            migrationBuilder.DropTable(
                name: "SystemUsersProfile");
        }
    }
}
