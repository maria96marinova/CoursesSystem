using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoursesSystem.Data.Migrations
{
    public partial class ReviewsTableStudentColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StudentId",
                table: "Reviews",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_StudentId",
                table: "Reviews",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_StudentId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_StudentId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Reviews");
        }
    }
}
