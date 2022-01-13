using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNPL_Web.DatabaseModels.Migrations
{
    public partial class tbr2r : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVerify",
                table: "CustomerProfiles",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerify",
                table: "CustomerProfiles");
        }
    }
}
