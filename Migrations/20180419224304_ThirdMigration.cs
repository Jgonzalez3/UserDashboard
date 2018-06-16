using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace UserDashboard.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Messages_MessageId1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Messages_MessageId2",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId1",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId2",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MessageId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MessageId2",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId2",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "MessageId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "MessageId2",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "MessageId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MessageId",
                table: "Comments",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Messages_MessageId",
                table: "Comments",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Messages_MessageId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MessageId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "MessageId1",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MessageId2",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId2",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MessageId1",
                table: "Comments",
                column: "MessageId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MessageId2",
                table: "Comments",
                column: "MessageId2");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId1",
                table: "Comments",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId2",
                table: "Comments",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Messages_MessageId1",
                table: "Comments",
                column: "MessageId1",
                principalTable: "Messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Messages_MessageId2",
                table: "Comments",
                column: "MessageId2",
                principalTable: "Messages",
                principalColumn: "MessageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId1",
                table: "Comments",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId2",
                table: "Comments",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
