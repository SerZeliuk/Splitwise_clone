using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Splitwise_clone.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_CreatorUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CreatorUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Transactions");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreatedBy",
                table: "Transactions",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_CreatedBy",
                table: "Transactions",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_CreatedBy",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CreatedBy",
                table: "Transactions");

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
                name: "FK_Participants_Users_UserId",
                table: "Participants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_CreatorUserId",
                table: "Transactions",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
