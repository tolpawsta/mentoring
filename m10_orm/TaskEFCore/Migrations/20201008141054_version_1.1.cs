using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskEFCore.Migrations
{
    public partial class version_11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeCreditCards",
                columns: table => new
                {
                    CardNumber = table.Column<long>(type: "bigint", maxLength: 16, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "date", maxLength: 4, nullable: false),
                    CardHolderName = table.Column<string>(maxLength: 40, nullable: true),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CardNumber", x => x.CardNumber);
                    table.ForeignKey(
                        name: "FK_EmployeeCreditCards_Empoyees",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCreditCards_CardHolderName",
                table: "EmployeeCreditCards",
                column: "CardHolderName");

            migrationBuilder.CreateIndex(
                name: "EmployeeCards",
                table: "EmployeeCreditCards",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeCreditCards");
        }
    }
}
