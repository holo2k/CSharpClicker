using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpClicker.Web.Migrations
{
    /// <inheritdoc />
    public partial class UserBoostsConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBoosts_AspNetUsers_ApplicationUserId",
                table: "UserBoosts");

            migrationBuilder.DropIndex(
                name: "IX_UserBoosts_ApplicationUserId",
                table: "UserBoosts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserBoosts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "UserBoosts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBoosts_ApplicationUserId",
                table: "UserBoosts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoosts_AspNetUsers_ApplicationUserId",
                table: "UserBoosts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
