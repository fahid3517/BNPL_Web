using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNPL_Web.DatabaseModels.Migrations
{
    public partial class tbl432 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerProfile",
                table: "CustomerProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetProfile",
                table: "AspNetProfile");

            migrationBuilder.RenameTable(
                name: "CustomerProfile",
                newName: "CustomerProfiles");

            migrationBuilder.RenameTable(
                name: "AspNetProfile",
                newName: "AspNetProfiles");

            migrationBuilder.RenameColumn(
                name: "MiddleNameEN",
                table: "CustomerProfiles",
                newName: "MiddleNameEn");

            migrationBuilder.RenameColumn(
                name: "MiddleNameAR",
                table: "CustomerProfiles",
                newName: "MiddleNameAr");

            migrationBuilder.RenameColumn(
                name: "LastNameEN",
                table: "CustomerProfiles",
                newName: "LastNameEn");

            migrationBuilder.RenameColumn(
                name: "LastNameAR",
                table: "CustomerProfiles",
                newName: "LastNameAr");

            migrationBuilder.RenameColumn(
                name: "FirstNameEN",
                table: "CustomerProfiles",
                newName: "FirstNameEn");

            migrationBuilder.RenameColumn(
                name: "FirstNameAR",
                table: "CustomerProfiles",
                newName: "FirstNameAr");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "AspNetProfileRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "AspNetProfileRoles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerProfiles",
                table: "CustomerProfiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetProfiles",
                table: "AspNetProfiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDisable = table.Column<bool>(type: "bit", nullable: false),
                    FirstLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SuccessFullLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLogout = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetProfileRoles_ProfileId",
                table: "AspNetProfileRoles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetProfileRoles_RoleId",
                table: "AspNetProfileRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetProfileRoles_AspNetProfiles_RoleId",
                table: "AspNetProfileRoles",
                column: "RoleId",
                principalTable: "AspNetProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetProfileRoles_AspNetRoles_ProfileId",
                table: "AspNetProfileRoles",
                column: "ProfileId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetProfileRoles_AspNetProfiles_RoleId",
                table: "AspNetProfileRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetProfileRoles_AspNetRoles_ProfileId",
                table: "AspNetProfileRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetProfileRoles_ProfileId",
                table: "AspNetProfileRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetProfileRoles_RoleId",
                table: "AspNetProfileRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerProfiles",
                table: "CustomerProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetProfiles",
                table: "AspNetProfiles");

            migrationBuilder.RenameTable(
                name: "CustomerProfiles",
                newName: "CustomerProfile");

            migrationBuilder.RenameTable(
                name: "AspNetProfiles",
                newName: "AspNetProfile");

            migrationBuilder.RenameColumn(
                name: "MiddleNameEn",
                table: "CustomerProfile",
                newName: "MiddleNameEN");

            migrationBuilder.RenameColumn(
                name: "MiddleNameAr",
                table: "CustomerProfile",
                newName: "MiddleNameAR");

            migrationBuilder.RenameColumn(
                name: "LastNameEn",
                table: "CustomerProfile",
                newName: "LastNameEN");

            migrationBuilder.RenameColumn(
                name: "LastNameAr",
                table: "CustomerProfile",
                newName: "LastNameAR");

            migrationBuilder.RenameColumn(
                name: "FirstNameEn",
                table: "CustomerProfile",
                newName: "FirstNameEN");

            migrationBuilder.RenameColumn(
                name: "FirstNameAr",
                table: "CustomerProfile",
                newName: "FirstNameAR");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetProfileRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileId",
                table: "AspNetProfileRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerProfile",
                table: "CustomerProfile",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetProfile",
                table: "AspNetProfile",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AspNetUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    FirstLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDisable = table.Column<bool>(type: "bit", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLogout = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuccessFullLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUser", x => x.Id);
                });
        }
    }
}
