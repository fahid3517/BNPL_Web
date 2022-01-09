using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNPL_Web.DatabaseModels.Migrations
{
    public partial class _123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePrivigaes_Privilages_RolesId",
                table: "RolePrivigaes");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePrivigaes_Roles_ProfileId",
                table: "RolePrivigaes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMemberships",
                table: "UserMemberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePrivigaes",
                table: "RolePrivigaes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Privilages",
                table: "Privilages");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserProfiles");

            migrationBuilder.RenameTable(
                name: "UserMemberships",
                newName: "AspNetMembership");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "AspNetProfile");

            migrationBuilder.RenameTable(
                name: "RolePrivigaes",
                newName: "AspNetProfileRoles");

            migrationBuilder.RenameTable(
                name: "Privilages",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_RolePrivigaes_RolesId",
                table: "AspNetProfileRoles",
                newName: "IX_AspNetProfileRoles_RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePrivigaes_ProfileId",
                table: "AspNetProfileRoles",
                newName: "IX_AspNetProfileRoles_ProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetMembership",
                table: "AspNetMembership",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetProfile",
                table: "AspNetProfile",
                column: "ProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetProfileRoles",
                table: "AspNetProfileRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetProfileRoles_AspNetProfile_ProfileId",
                table: "AspNetProfileRoles",
                column: "ProfileId",
                principalTable: "AspNetProfile",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetProfileRoles_AspNetRoles_RolesId",
                table: "AspNetProfileRoles",
                column: "RolesId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetProfileRoles_AspNetProfile_ProfileId",
                table: "AspNetProfileRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetProfileRoles_AspNetRoles_RolesId",
                table: "AspNetProfileRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetProfileRoles",
                table: "AspNetProfileRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetProfile",
                table: "AspNetProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetMembership",
                table: "AspNetMembership");

            migrationBuilder.RenameTable(
                name: "UserProfiles",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Privilages");

            migrationBuilder.RenameTable(
                name: "AspNetProfileRoles",
                newName: "RolePrivigaes");

            migrationBuilder.RenameTable(
                name: "AspNetProfile",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "AspNetMembership",
                newName: "UserMemberships");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetProfileRoles_RolesId",
                table: "RolePrivigaes",
                newName: "IX_RolePrivigaes_RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetProfileRoles_ProfileId",
                table: "RolePrivigaes",
                newName: "IX_RolePrivigaes_ProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Privilages",
                table: "Privilages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePrivigaes",
                table: "RolePrivigaes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "ProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMemberships",
                table: "UserMemberships",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePrivigaes_Privilages_RolesId",
                table: "RolePrivigaes",
                column: "RolesId",
                principalTable: "Privilages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePrivigaes_Roles_ProfileId",
                table: "RolePrivigaes",
                column: "ProfileId",
                principalTable: "Roles",
                principalColumn: "ProfileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
