using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BNPL_Web.DatabaseModels.Migrations
{
    public partial class ChangesLogTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CusTransactionsLogs",
                table: "CusTransactionsLogs");

            migrationBuilder.RenameTable(
                name: "CusTransactionsLogs",
                newName: "LogsCheckout");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogsCheckout",
                table: "LogsCheckout",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LogsCheckout",
                table: "LogsCheckout");

            migrationBuilder.RenameTable(
                name: "LogsCheckout",
                newName: "CusTransactionsLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CusTransactionsLogs",
                table: "CusTransactionsLogs",
                column: "Id");
        }
    }
}