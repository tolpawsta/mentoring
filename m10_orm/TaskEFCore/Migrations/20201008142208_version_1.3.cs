using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskEFCore.Migrations
{
    public partial class version_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Region",
                newName: "Regions");

            migrationBuilder.AddColumn<DateTime>(
                name: "FoundationDate",
                table: "Customers",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoundationDate",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Region");            
        }
    }
}
