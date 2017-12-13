using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eventy.Data.Migrations.Eventy
{
    public partial class UpdateFamilyMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "FamilyMembers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "FamilyMembers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCreated",
                table: "FamilyMembers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserModified",
                table: "FamilyMembers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "UserCreated",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "UserModified",
                table: "FamilyMembers");
        }
    }
}
