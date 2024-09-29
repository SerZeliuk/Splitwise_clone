using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Splitwise_clone.Migrations
{
    /// <inheritdoc />
    public partial class TransactionCreatorAndOther : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date",
                table: "Transactions",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreatorUserId",
                table: "Transactions",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_CreatorUserId",
                table: "Transactions",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_CreatorUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CreatorUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Transactions",
                newName: "date");
        }
    }
}
